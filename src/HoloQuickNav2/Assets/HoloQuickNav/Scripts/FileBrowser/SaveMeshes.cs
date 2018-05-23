using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveMeshes {

    /*
    public static void SaveMesh(string path, Mesh mesh)
    {
        byte[] bytes = MeshSerializer.WriteMesh(mesh, true);
        File.WriteAllBytes(path, bytes);
    }

    public static Mesh LoadMesh(string path)
    {
        if (File.Exists(path) == true)
        {
            byte[] bytes = File.ReadAllBytes(path);
            return MeshSerializer.ReadMesh(bytes);
        }
        return null;
    }
    */

    /*
    public static void SaveMesh(string path, Mesh myMesh)
    {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
        SerializableMeshInfo smi = new SerializableMeshInfo(myMesh);
        bf.Serialize(fs, smi);
        fs.Close();
    }
    /// <summary>
    /// Loads a mesh from a binary dump
    /// </summary>
    public static Mesh LoadMesh(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError("meshFile.dat file does not exist.");
            return null;
        }
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
        SerializableMeshInfo smi = (SerializableMeshInfo)bf.Deserialize(fs);
        Mesh myMesh = smi.GetMesh();
        fs.Close();
        return myMesh;
        
    }
    */
}
