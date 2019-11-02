using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Data.Providers.DB
{
    /// <summary>
    /// базовые функции для работы файловой БД
    /// </summary>
    /// <typeparam name="TKey">ключ по которому хранятся данные</typeparam>
    /// <typeparam name="TData">тип данных в БД</typeparam>
    public abstract class BaseFileDatabase<TKey, TData>
    {

        /// <summary>
        /// создает объект для этого файла, не загружая данные
        /// </summary>
        /// <param name="FileName"></param>
        public BaseFileDatabase(string FileName)
        {
            this.FileName = FileName;
        }

        /// <summary>
        /// Исходные данные БД
        /// </summary>
        protected Dictionary<TKey, TData> _dictionary = null;

        /// <summary>
        /// список значений
        /// </summary>
        public Dictionary<TKey, TData> Dictionary
        {
            get
            {
                if (_dictionary == null || _dictionary.Count == 0)
                    _dictionary = LoadDatabaseFile();
                return _dictionary;
            }
        }

        /// <summary>
        /// Возвращает данные о МС по ключу
        /// </summary>
        /// <param name="id">ключ, по которому харанятся данные</param>
        /// <returns></returns>
        public TData this[TKey id] { get { return _dictionary[id]; } }


        /// <summary>
        /// Исходные данные БД
        /// </summary>
        protected List<TData> _list = null;

        /// <summary>
        /// список значений
        /// </summary>
        public List<TData> List
        {
            get
            {
                if (_list == null || _list.Count == 0)
                    _list = Dictionary.Values.ToList();
                return _list;
            }
        }

        /// <summary>
        /// путь к файлу БД
        /// </summary>
        public string FileName { get; protected set; }

        /// <summary>
        /// пробует загрузить файл ограничений и возвращает true, если  загрузка удалась
        /// </summary>
        /// <returns></returns>
        public bool CheckDatabaseFile()
        {
            try
            {
                LoadDatabaseFile();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        /// <summary>
        /// метод, загружающий файл в память
        /// </summary>
        /// <returns></returns>
        public abstract Dictionary<TKey, TData> LoadDatabaseFile();

    }
}
