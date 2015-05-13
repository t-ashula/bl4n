// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionalParams.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> �K�{�ł͂Ȃ��p�����[�^�̕ύX��ǐՂ��܂� </summary>
    public abstract class OptionalParams
    {
        private readonly Dictionary<string, bool> _changing;

        /// <summary> <see cref="OptionalParams"/> �̃C���X�^���X�����������܂� </summary>
        /// <param name="keys"> �ύX��ǐՂ���v���p�e�B�� </param>
        protected OptionalParams(params string[] keys)
        {
            _changing = new Dictionary<string, bool>();
            AddTrackingKeys(keys);
        }

        /// <summary> �ύX��ǐՂ���v���p�e�B����ǉ����܂� </summary>
        /// <param name="keys"> �ύX��ǐՂ���v���p�e�B�� </param>
        protected void AddTrackingKeys(params string[] keys)
        {
            foreach (var key in keys)
            {
                _changing.Add(key, false);
            }
        }

        /// <summary> <paramref name="key"/> ���ύX���ꂽ���ǂ����𓾂܂� </summary>
        /// <param name="key">�v���p�e�B��</param>
        /// <returns> �ύX���ꂽ�� true </returns>
        protected bool IsPropertyChanged(string key)
        {
            return _changing[key];
        }

        /// <summary> <paramref name="key"/> ���ύX���ꂽ�Ƃ��ɌĂт܂� </summary>
        /// <param name="key"> �ύX���ꂽ�v���p�e�B�� </param>
        protected void PropertyChanged(string key)
        {
            _changing[key] = true;
        }
    }
}