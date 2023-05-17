using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // 리스트를 쓰기 위해 추가합니다.

// BinaryFormatter를 사용하기 위해서는 반드시 아래의 네임스페이스를 추가해줘야 해요.
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
    // 만든 클래스를 리스트에 담아서 관리하면 마치 테이블처럼 사용할 수 있습니다. 
    private void SaveInven()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // score를 바이트 배열로 변환해서 저장합니다.
        binaryFormatter.Serialize(memoryStream, InvenData.items);

        // 그것을 다시 한번 문자열 값으로 변환해서 
        // 'HighScore'라는 스트링 키값으로 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetString("SaveInven", Convert.ToBase64String(memoryStream.GetBuffer()));
    }
    private void SaveStorage()
    {
        var binaryFormatter = new BinaryFormatter();
        var memoryStream = new MemoryStream();

        // score를 바이트 배열로 변환해서 저장합니다.
        binaryFormatter.Serialize(memoryStream, StorData.items);

        // 그것을 다시 한번 문자열 값으로 변환해서 
        // 'HighScore'라는 스트링 키값으로 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetString("SaveStorage", Convert.ToBase64String(memoryStream.GetBuffer()));
    }

    public void LoadInven()
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveInven");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            InvenData.items = (List<Item>)binaryFormatter.Deserialize(memoryStream);
        }
    }
    public void LoadStorage()
    {
        // 'SaveInven' 스트링 키값으로 데이터를 가져옵니다.
        var data = PlayerPrefs.GetString("SaveStorage");
        if (!string.IsNullOrEmpty(data))
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream(Convert.FromBase64String(data));

            // 가져온 데이터를 바이트 배열로 변환하고
            // 사용하기 위해 다시 리스트로 캐스팅해줍니다.
            StorData.items = (List<Item>)binaryFormatter.Deserialize(memoryStream);
        }
    }
}
