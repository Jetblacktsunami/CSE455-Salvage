using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;

[ExecuteInEditMode]
public class XMLFileManager : MonoBehaviour 
{
	public static void EncryptFile (string path)
	{
		if (!File.Exists(path))
		{
			Debug.Log("File Not Found At: " + path);
			return;
		}
		
		XDocument xmlFile = XDocument.Load(path);
		XmlReader reader = xmlFile.CreateReader();
		reader.MoveToContent();
		if(xmlFile.Root.HasElements)
		{
			Debug.Log("encrypting");
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("86759426197648123460789546213421");
			byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes (reader.ReadInnerXml());
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateEncryptor();
			byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
			xmlFile.Root.ReplaceNodes(Convert.ToBase64String (resultArray, 0, resultArray.Length));
		}

		xmlFile.Save(path);
	}
	
	public static void DecryptFile (string path)
	{
		if (!File.Exists(path))
		{
			Debug.Log("File Not Found At: " + path);
			return;
		}

		XDocument xmlFile = XDocument.Load (path);
		XmlReader reader = xmlFile.CreateReader ();

		reader.MoveToContent ();
		if(xmlFile.Root.Elements() != null)
		{
			Debug.Log("decrypting");
			byte[] keyArray = UTF8Encoding.UTF8.GetBytes ("86759426197648123460789546213421");
			byte[] toEncryptArray = Convert.FromBase64String (reader.ReadInnerXml());
			RijndaelManaged rDel = new RijndaelManaged();
			rDel.Key = keyArray;
			rDel.Mode = CipherMode.ECB;
			rDel.Padding = PaddingMode.PKCS7;
			ICryptoTransform cTransform = rDel.CreateDecryptor();
			byte[] resultArray = cTransform.TransformFinalBlock (toEncryptArray, 0, toEncryptArray.Length);
			string data = (UTF8Encoding.UTF8.GetString (resultArray));
			data = "<Root>" + data + "</Root>";
			xmlFile = (XDocument.Parse(data));

		}

		xmlFile.Save (path);
	}
}