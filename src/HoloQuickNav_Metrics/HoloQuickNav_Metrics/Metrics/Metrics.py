import os
import unittest
import vtk, qt, ctk, slicer
from slicer.ScriptedLoadableModule import *
import logging
import math

#
# Metrics
#

class Metrics(ScriptedLoadableModule):
  """Uses ScriptedLoadableModule base class, available at:
  https://github.com/Slicer/Slicer/blob/master/Base/Python/slicer/ScriptedLoadableModule.py
  """

  def __init__(self, parent):
    ScriptedLoadableModule.__init__(self, parent)
    self.parent.title = "Metrics"
    self.parent.categories = ["HoloQuickNav"]
    self.parent.dependencies = []
    self.parent.contributors = ["Zachary Baum (PerkLab)"]
    self.parent.helpText = """
    """
    self.parent.helpText += self.getDefaultModuleDocumentationLink()
    self.parent.acknowledgementText = """
    """

#
# MetricsWidget
#

class MetricsWidget(ScriptedLoadableModuleWidget):
  """Uses ScriptedLoadableModuleWidget base class, available at:
  https://github.com/Slicer/Slicer/blob/master/Base/Python/slicer/ScriptedLoadableModule.py
  """

  def setup(self):
    ScriptedLoadableModuleWidget.setup(self)

    # Instantiate and connect widgets ...

    #
    # Parameters Area
    #
    parametersCollapsibleButton = ctk.ctkCollapsibleButton()
    parametersCollapsibleButton.text = "Parameters"
    self.layout.addWidget(parametersCollapsibleButton)

    # Layout within the dummy collapsible button
    parametersFormLayout = qt.QFormLayout(parametersCollapsibleButton)

    #
    # model selector
    #
    self.modelSelector = slicer.qMRMLNodeComboBox()
    self.modelSelector.nodeTypes = ["vtkMRMLModelNode"]
    self.modelSelector.selectNodeUponCreation = True
    self.modelSelector.addEnabled = False
    self.modelSelector.removeEnabled = False
    self.modelSelector.noneEnabled = False
    self.modelSelector.showHidden = False
    self.modelSelector.showChildNodeTypes = False
    self.modelSelector.setMRMLScene( slicer.mrmlScene )
    self.modelSelector.setToolTip( "Pick the input to the algorithm." )
    parametersFormLayout.addRow("Input Volume: ", self.modelSelector)

    #
    # markups selector
    #
    self.markupsSelector = slicer.qMRMLNodeComboBox()
    self.markupsSelector.nodeTypes = ["vtkMRMLMarkupsFiducialNode"]
    self.markupsSelector.selectNodeUponCreation = True
    self.markupsSelector.addEnabled = False
    self.markupsSelector.removeEnabled = False
    self.markupsSelector.noneEnabled = False
    self.markupsSelector.showHidden = False
    self.markupsSelector.showChildNodeTypes = False
    self.markupsSelector.setMRMLScene( slicer.mrmlScene )
    self.markupsSelector.setToolTip( "Pick the output to the algorithm." )
    parametersFormLayout.addRow("Output Volume: ", self.markupsSelector)

    #
    # Apply Button
    #
    self.applyButton = qt.QPushButton("Apply")
    self.applyButton.toolTip = "Compute Metrics."
    self.applyButton.enabled = False
    parametersFormLayout.addRow(self.applyButton)

    #
    # point-to-point distances results table
    #
    self.resultsTable = qt.QTableWidget()
    self.resultsTable.setRowCount(0)
    self.resultsTable.setColumnCount(0)
    parametersFormLayout.addRow(qt.QLabel('Point to Point Metrics:'))
    parametersFormLayout.addRow(self.resultsTable)

    # connections
    self.applyButton.connect('clicked(bool)', self.onApplyButton)
    self.modelSelector.connect("currentNodeChanged(vtkMRMLNode*)", self.onSelect)
    self.markupsSelector.connect("currentNodeChanged(vtkMRMLNode*)", self.onSelect)

    # Add vertical spacer
    self.layout.addStretch(1)

    # Refresh Apply button state
    self.onSelect()

  def cleanup(self):
    pass

  def onSelect(self):
    self.applyButton.enabled = self.modelSelector.currentNode() and self.markupsSelector.currentNode()

  def onApplyButton(self):
    logic = MetricsLogic()
    logic.run(self.modelSelector.currentNode(), self.markupsSelector.currentNode(), self.resultsTable)

#
# MetricsLogic
#

class MetricsLogic(ScriptedLoadableModuleLogic):

  def GetModelCenterOfMass(self, model):
    """
    Finds the center of mass for the provided model.
    :param model: A vtkMRMLModelNode of the anatomy of interest we want to find the center of.
    :return: List containing the RAS coordinates of the model's center of mass.
    """
    polyData = model.GetPolyData()
    comFilter = vtk.vtkCenterOfMass()
    comFilter.SetInputData(polyData)
    comFilter.SetUseScalarsAsWeights(False)
    comFilter.Update()
    comPoint = comFilter.GetCenter()
    return comPoint

  def CreateCenterOfMassFiducial(self, markups, comPoint):
    """
    Creates a markups fiducial at the center of mass of the provided model.
    :param comPoint: List containing the RAS coordinates of the model's center of mass.
    :param markups: Markups list to add the comPoint to.
    :return: Void.
    """
    comIndex = None
    for i in range(markups.GetNumberOfFiducials()):
      if markups.GetNthFiducialLabel(i) in 'Model_COM':
        comIndex = i
    if comIndex is not None:
      markups.RemoveMarkup(comIndex)
    markups.AddFiducialFromArray(comPoint, 'Model_COM')

  def GetPointToPointDistance(self, point1, point2):
    """
    Calculates the point-point distance between provided inputs.
    :param point1: List containing the RAS coordinates of the first point.
    :param point2: List containing the RAS coordinates of the second point.
    :return: The distance between the two points.
    """
    return math.sqrt(vtk.vtkMath.Distance2BetweenPoints(point1, point2))

  def PopulatePointToPointDistancesResultsTable(self, markups, table):
    """
    Calculates the point-to-point distances between each point in the markups list and
    puts them into a QTableWidget in the GUI.
    :param markups: The Markups list we are finding the point-to-point distances from.
    :param table: The results table widget to populate.
    :return: Void.
    """
    headerList = [markups.GetNthFiducialLabel(i) for i in range(markups.GetNumberOfFiducials())]
    table.setRowCount(len(headerList))
    table.setColumnCount(len(headerList))
    table.setHorizontalHeaderLabels(headerList)
    table.setVerticalHeaderLabels(headerList)

    for point1 in range(markups.GetNumberOfFiducials()):
      for point2 in range(point1 + 1, markups.GetNumberOfFiducials()):
        point1_RAS = [0, 0, 0]
        point2_RAS = [0, 0, 0]
        markups.GetNthFiducialPosition(point1, point1_RAS)
        markups.GetNthFiducialPosition(point2, point2_RAS)
        distance = round(self.GetPointToPointDistance(point1_RAS, point2_RAS), 1)
        table.setItem(point1, point2, qt.QTableWidgetItem(str(distance)))

    table.show()
    table.resizeColumnsToContents()
    table.resizeRowsToContents()

  def run(self, model, markups, resultsTable):
    """
    Run the actual algorithm
    """
    comPoint = self.GetModelCenterOfMass(model)
    self.CreateCenterOfMassFiducial(markups, comPoint)
    self.PopulatePointToPointDistancesResultsTable(markups, resultsTable)

    return True