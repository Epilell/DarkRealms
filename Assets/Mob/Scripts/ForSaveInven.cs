using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // ����Ʈ�� ���� ���� �߰��մϴ�.

// BinaryFormatter�� ����ϱ� ���ؼ��� �ݵ�� �Ʒ��� ���ӽ����̽��� �߰������ �ؿ�.
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ForSaveInven : MonoBehaviour
{
    public InventoryData InvenData;
    public StorageData StorData;
    public void Save()
    {
        SaveInven();
        SaveStorage();
    }
    public void Load()
    {
        LoadInven();
        LoadStorage();
    }
    // ���� Ŭ������ ����Ʈ�� ��Ƽ� �����ϸ� ��ġ ���̺�ó�� ����� �� �ֽ��ϴ�. 
    private void SaveInven()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // score�� ����Ʈ �迭�� ��ȯ�ؼ� �����մϴ�.
        binaryFormatter.Serialize(memoryStream, InvenData.items);

        // �װ��� �ٽ� �ѹ� ���ڿ� ������ ��ȯ�ؼ� 
        // 'HighScore'��� ��Ʈ�� Ű������ PlayerPrefs�� �����մϴ�.
        PlayerPrefs.SetString("SaveInven", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    private void SaveStorage()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // score�� ����Ʈ �迭�� ��ȯ�ؼ� �����մϴ�.
        binaryFormatter.Serialize(memoryStream, StorData.items);

        // �װ��� �ٽ� �ѹ� ���ڿ� ������ ��ȯ�ؼ� 
        // 'HighScore'��� ��Ʈ�� Ű������ PlayerPrefs�� �����մϴ�.
        PlayerPrefs.SetString("SaveStorage", Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public void LoadInven()
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveInven");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            InvenData.items = (List<Item>)binaryFormatter.Deserialize(memoryStream);
        }
    }
    public void LoadStorage()
    {
        // 'SaveInven' ��Ʈ�� Ű������ �����͸� �����ɴϴ�.
        var data = PlayerPrefs.GetString("SaveStorage");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // ������ �����͸� ����Ʈ �迭�� ��ȯ�ϰ�
            // ����ϱ� ���� �ٽ� ����Ʈ�� ĳ�������ݴϴ�.
            StorData.items = (List<Item>)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
