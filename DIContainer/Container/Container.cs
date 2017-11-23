using System;
using System.Collections.Generic;
using System.Linq;
using DIContainer.Exceptions;

namespace DIContainer
{
    /// <summary>
    /// Самописный DI контейнер
    /// </summary>
    public class Container
    {
        //Словарь содержащий ключ и соответствующий ему тип
        private readonly Dictionary<Key, Type> _containerDictionary = new Dictionary<Key, Type>();

        private int _elementsAmount;

        public int Count => _elementsAmount;

        /// <summary>
        /// Регистрирует тип на самого себя
        /// </summary>
        /// <param name="key">Регистрируемый тип</param>
        public void Register(Type key)
        {
            var currentKey = new Key {KeyType = key};
            _containerDictionary.Add(currentKey, key);
            _elementsAmount++;
        }

        /// <summary>
        /// Регистрирует тип
        /// </summary>
        /// <param name="key">Интерфейс</param>
        /// <param name="value">Тип, который реализует регистрируемый интерфейс</param>
        public void Register(Type key, Type value)
        {
            try
            {
                Contains(value);
                NotImplemented(key, value);
            }
            catch (NotImplementedException e)
            {
                throw new InterfaceNotImplementedException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ElementAlreadyExistsException(e.Message);
            }
            var currentKey = new Key {KeyType = key};
            _containerDictionary.Add(currentKey, value);
            _elementsAmount++;
        }

        /// <summary>
        /// Добавляет связь интерфейс-реализация плюс описание
        /// </summary>
        /// <param name="key">ключ-интерфейс</param>
        /// <param name="value">реализация интерфейса</param>
        /// <param name="label">описание</param>
        public void Register(Type key, Type value, string label)
        {
            try
            {
                Contains(value);
                NotImplemented(key, value);
            }
            catch (NotImplementedException e)
            {
                throw new InterfaceNotImplementedException(e.Message);
            }
            catch (ArgumentException e)
            {
                throw new ElementAlreadyExistsException(e.Message);
            }
            var currentKey = new Key
            {
                KeyType = key,
                KeyName = label
            };
            _containerDictionary.Add(currentKey, value);
            _elementsAmount++;

        }

        /// <summary>
        /// Получает объект по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Полученный объект</returns>
        public object Resolve(Type key)
        {
            var currentKey = new Key
            {
                KeyType = key
            };

            if (_containerDictionary.ContainsKey(currentKey))
            {
                var value = _containerDictionary[currentKey];
                try
                {
                    CheckParameters(0, value);
                }
                catch (ArgumentException ex)
                {
                    throw new ElementNotFoundException(ex.Message);
                }
                return Activator.CreateInstance(value);
            }
            return null;
        }

        /// <summary>
        /// Возвращает объект по интерфейсу и дополнительному описанию
        /// </summary>
        /// <param name="key">ключ-интерфейс</param>
        /// <param name="label">описание</param>
        /// <returns>Объект, если найден, иначе null</returns>
        public object Resolve(Type key, string label)
        {
            var currentKey = new Key
            {
                KeyType = key,
                KeyName = label
            };
            if (_containerDictionary.ContainsKey(currentKey))
            {
                var value = _containerDictionary[currentKey];
                try
                {
                    CheckParameters(0, value);
                }
                catch (ArgumentException ex)
                {
                    throw new ElementNotFoundException(ex.Message);
                }
                return Activator.CreateInstance(value);
            }
            return null;
        }

        /// <summary>
        /// Получает объект с параметрами
        /// </summary>
        /// <param name="key">объект ключ</param>
        /// <param name="param">параметры</param>
        /// <returns></returns>
        public object Resolve(Type key, object[] param)
        {
            var currentKey = new Key();
            currentKey.KeyType = key;

            if (_containerDictionary.ContainsKey(currentKey))
            {
                var value = _containerDictionary[currentKey];
                try
                {
                    CheckParameters(param.Length, value);
                }
                catch (ArgumentException ex)
                {
                    throw new ElementNotFoundException(ex.Message);
                }
                return Activator.CreateInstance(value, param);
            }

            return null;

        }

        /// <summary>
        /// Получает объект с параметрами и описанием
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="param">Параметры</param>
        /// <param name="label">Необязательное описание</param>
        /// <returns></returns>
        public object Resolve(Type key, object[] param, string label)
        {

            var keystruct = new Key
            {
                KeyType = key,
                KeyName = label
            };
            if (_containerDictionary.ContainsKey(keystruct))
            {
                var value = _containerDictionary[keystruct];
                try
                {
                    CheckParameters(param.Length, value);
                }
                catch (ArgumentException ex)
                {
                    throw new ElementNotFoundException(ex.Message);
                }
                return Activator.CreateInstance(value, param);
            }

            return null;
        }

        /// <summary>
        /// Проверяет, содержится ли в словаре тип
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool Contains(Type value)
        {
            if (_containerDictionary.ContainsValue(value))
            {
                throw new ElementAlreadyExistsException(value.Name + " already exists");
            }
            else
                return false;
        }

        /// <summary>
        /// Проверяет, реализован ли интерфейс классом
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool NotImplemented(Type key, Type value)
        {
            if (value.GetInterfaces().Contains(key))
                return true;
            throw new NotImplementedException("Interface not implemented");
            
        }

        /// <summary>
        /// Проверяет наличие конструктора класса по параметрам
        /// </summary>
        /// <param name="count">параметры конструктора</param>
        /// <param name="type">заданный класс</param>
        /// <returns>true в случае найденного класса</returns>
        private bool CheckParameters(int count, Type type)
        {
            var arrType = type.GetConstructors();

            foreach (var value in arrType)
            {
                var param = value.GetParameters();
                if (param.Length == count)
                    return true;
            }
            throw new ArgumentException("Constructor not found");
        }
    }
}
