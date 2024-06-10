using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class SaveData
{
  public static string path = Application.persistentDataPath + "/savedata";

  public MemoryStream stream = new MemoryStream();
  public BinaryReader reader;
  public BinaryWriter writer;

  public SaveData()
  {
    this.reader = new BinaryReader(this.stream);
    this.writer = new BinaryWriter(this.stream);
  }

  public SaveData(byte[] data)
  {
    this.reader = new BinaryReader(this.stream);
    this.writer = new BinaryWriter(this.stream);
    this.stream.Write(data, 0, data.Length);
    this.stream.Position = 0L;
  }

  public void Write(string data) => writer.Write(data);
  public void Write(float data) => writer.Write(data);
  public void Write(int data) => writer.Write(data);
  public void Write(uint data) => writer.Write(data);

  public void Write(bool data) => writer.Write(data);

  public void Write(Guid id) => writer.Write(id.ToString());

  public void Write(Vector2Int vector)
  {
    writer.Write(vector.x);
    writer.Write(vector.y);
  }
  public void Write(Vector3 vector)
  {
    writer.Write(vector.x);
    writer.Write(vector.y);
    writer.Write(vector.z);
  }

  public void Write(Quaternion quaternion)
  {
    writer.Write(quaternion.w);
    writer.Write(quaternion.x);
    writer.Write(quaternion.y);
    writer.Write(quaternion.z);
  }

  public string ReadString() => reader.ReadString();
  public int ReadInt() => reader.ReadInt32();
  public float ReadFloat() => reader.ReadSingle();

  public bool ReadBool() => reader.ReadBoolean();

  public Guid ReadId() => Guid.Parse(reader.ReadString());
  public uint ReadUInt() => reader.ReadUInt32();

  public Vector2Int ReadVector2Int() => new Vector2Int()
  {
    x = reader.ReadInt32(),
    y = reader.ReadInt32()
  };
  public Vector3 ReadVector3() => new Vector3()
  {
    x = reader.ReadSingle(),
    y = reader.ReadSingle(),
    z = reader.ReadSingle()
  };

  public Quaternion ReadQuaternion() => new Quaternion()
  {
    w = reader.ReadSingle(),
    x = reader.ReadSingle(),
    y = reader.ReadSingle(),
    z = reader.ReadSingle(),
  };

  private byte[] GetArray()
  {
    writer.Flush();
    stream.Flush();
    return stream.ToArray();
  }

  private byte[] GenerateHash()
  {
    byte[] array = this.GetArray();
    return SHA512.Create().ComputeHash(array);
  }

  public void WriteToDisk(string path)
  {
    byte[] hash = GenerateHash();
    byte[] array = GetArray();
    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(SaveData.path);
    }
    FileStream fileStream = File.Create(SaveData.path + path);
    BinaryWriter binaryWriter = new BinaryWriter(fileStream);
    binaryWriter.Write(array.Length);
    binaryWriter.Write(array);
    binaryWriter.Write(hash.Length);
    binaryWriter.Write(hash);
    binaryWriter.Flush();
    fileStream.Flush(true);
    fileStream.Close();
    fileStream.Dispose();
  }

  public static SaveData ReadFromDisk(string path)
  {
    FileStream fileStream;
    try
    {
      fileStream = File.OpenRead(SaveData.path + path);
    }
    catch { return null; }
    byte[] data;
    try
    {
      BinaryReader binaryReader = new BinaryReader((Stream)fileStream);
      data = binaryReader.ReadBytes(binaryReader.ReadInt32());
      binaryReader.ReadBytes(binaryReader.ReadInt32());
    }
    catch
    {
      fileStream.Dispose();
      return null;
    }
    fileStream.Dispose();
    return new SaveData(data);
  }

}
