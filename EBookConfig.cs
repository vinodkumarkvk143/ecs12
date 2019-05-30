using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace Squarepanda.Ebook.Config
{

    #region EBookConfig Singleton Instance
    public sealed partial class EBookConfig
    {
        public static readonly EBookConfig Instance = new EBookConfig();
    }
    #endregion

    public sealed partial class EBookConfig
    {
        #region Private data
        private List<EBook> m_Ebooks; // not using now...

        private EBook m_CurrentEbook;
        #endregion

        #region Public Variable
        public List<EBook> GetAllEbooks()
        {
            return m_Ebooks;
        }

        public void SetCurrentEBook(EBook ebook)
        {
            m_CurrentEbook = ebook;
        }

        public EBook GetCurrentBook()

        {
            return m_CurrentEbook;
        }

        public List<BookPages> GetAllPage()
        {
            return m_CurrentEbook.pages;
        }

        #endregion

        public void FetchEBookConfigData(Action onSuccess, Action<string> OnFailure)
        {
            try
            {
                string path = Application.dataPath + "/ebook.json";

                string data = File.ReadAllText(path);

                EBook response = JsonConvert.DeserializeObject<EBook>(data);

                SetCurrentEBook(response);
                onSuccess?.Invoke();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                OnFailure?.Invoke(e.Message);
            }
        }
    }
}