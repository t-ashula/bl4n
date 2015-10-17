// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtraJsonPropertyReadableObject.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BL4N.Data
{
    /// <summary> �N���X�̃����o�[�Ƀf�V���A���C�Y����Ȃ����� JSON �̃v���p�e�B���擾�\�ȃI�u�W�F�N�g��\���܂� </summary>
    public abstract class ExtraJsonPropertyReadableObject
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> extraProperties = new Dictionary<string, JToken>();

        /// <summary> �]���ȃv���p�e�B�Ƃ��̒l�𕶎���̃y�A�œ��܂� </summary>
        /// <returns> �]���ȃv���p�e�B�̈ꗗ </returns>
        public IDictionary<string, string> GetExtraProperties()
        {
            return extraProperties.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
        }

        /// <summary>
        /// �]���ȃv���p�e�B�����邩�ǂ������擾���܂�
        /// </summary>
        /// <returns> �]���ȃv���p�e�B������Ƃ� true </returns>
        public bool HasExtraProperty()
        {
            return extraProperties.Any();
        }
    }
}