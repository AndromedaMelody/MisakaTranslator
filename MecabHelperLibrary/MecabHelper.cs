﻿using MeCab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecabHelperLibrary
{
    public struct MecabWordInfo {

        /// <summary>
        /// 单词
        /// </summary>
        public string Word;

        /// <summary>
        /// 词性
        /// </summary>
        public string PartOfSpeech;

        /// <summary>
        /// 词性说明
        /// </summary>
        public string Description;

        /// <summary>
        /// 假名
        /// </summary>
        public string Kana;

        /// <summary>
        /// Mecab能提供的关于这个词的详细信息 CSV表示
        /// </summary>
        public string Feature;
    }


    public class MecabHelper:IDisposable
    {
        private MeCabTagger Tagger;

        public MecabHelper(string dicPath) {
            if (dicPath != string.Empty)
            {
                Tagger = MeCabTagger.Create(
                    new MeCabParam()
                    {
                        DicDir = dicPath
                    }
                );
            }
        }

        public void Dispose()
        {
            Tagger?.Dispose();
        }

        /// <summary>
        /// 处理句子，对句子进行分词，得到结果
        /// </summary>
        /// <param name="sentence"></param>
        /// <returns></returns>
        public List<MecabWordInfo> SentenceHandle(string sentence) {

            List<MecabWordInfo> ret = new List<MecabWordInfo>();

            foreach (var node in Tagger.ParseToNodes(sentence))
            {
                if (node.CharType > 0)
                {
                    var features = node.Feature.Split(',');

                    
                    MecabWordInfo mwi = new MecabWordInfo {
                        Word = node.Surface,
                        PartOfSpeech = features[0],
                        Description = features[1],
                        Feature = node.Feature
                    };

                    //加这一步是为了防止乱码进入分词导致无法读取假名
                    if (features.Length >= 8)
                    {
                        mwi.Kana = features[7];
                    }
                    
                    ret.Add(mwi);
                }
            }

            return ret;
        }


    }
}
