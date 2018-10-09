cmake_minimum_required(VERSION 3.5)

project(HoloQuickNav_Metrics)

#-----------------------------------------------------------------------------
# Extension meta-information
set(EXTENSION_HOMEPAGE "http://slicer.org/slicerWiki/index.php/Documentation/Nightly/Extensions/HoloQuickNav_Metrics")
set(EXTENSION_CATEGORY "HoloQuickNav")
set(EXTENSION_CONTRIBUTORS "Zachary Baum (PerkLab)")
set(EXTENSION_DESCRIPTION "Module to help with calculations of metrics for HoloQuickNav results.")
set(EXTENSION_ICONURL "http://www.example.com/Slicer/Extensions/HoloQuickNav_Metrics.png")
set(EXTENSION_SCREENSHOTURLS "http://www.example.com/Slicer/Extensions/HoloQuickNav_Metrics/Screenshots/1.png")
set(EXTENSION_DEPENDS "NA") # Specified as a space separated string, a list or 'NA' if any

#-----------------------------------------------------------------------------
# Extension dependencies
find_package(Slicer REQUIRED)
include(${Slicer_USE_FILE})

#-----------------------------------------------------------------------------
# Extension modules
add_subdirectory(Metrics)
## NEXT_MODULE

#-----------------------------------------------------------------------------
include(${Slicer_EXTENSION_GENERATE_CONFIG})
include(${Slicer_EXTENSION_CPACK})
