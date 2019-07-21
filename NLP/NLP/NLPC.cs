using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NaturalLanguageProcessing
{
    namespace Tokenize
    {
        public class Token
        {
            private const string punctuation = ".,:;?()[]\"`'!_-/\\+*@#$%^&~={}|<>";
            private bool isPunctuation(char character)
            {
                for (int i = 0; i < punctuation.Length; i++)
                    if (character == punctuation[i])
                        return true;
                return false;
            }
            public List<string> treebankWordTokenizer(string token)
            {
                List<string> words = new List<string>();
                bool reading_word = false;
                string word = "";
                for (int i = 0; i < token.Length; i++)
                {
                    if (!reading_word)
                    {
                        if (Char.IsLetterOrDigit(token[i]) && !isPunctuation(token[i]))
                        {
                            reading_word = true;
                            word += token[i];
                        }
                        if (isPunctuation(token[i]))
                            words.Add(token[i].ToString());
                    }
                    else // reading_word == true
                    {
                        if (i == (token.Length - 1))
                        {
                            if(isPunctuation(token[i]))
                            {
                                words.Add(word);
                                words.Add(token[i].ToString());
                                break;
                            }
                            if (token[i] == ' ' || token[i] == '\n' || token[i] == '\t' || token[i] == '\r')
                                words.Add(word);
                            else if(Char.IsLetterOrDigit(token[i]))
                            {
                                word += token[i];
                                words.Add(word);
                            }
                            break;
                        }
                        if (token[i] == ' ' || token[i] == '\n' || token[i] == '\t' || token[i] == '\r')
                        {
                            words.Add(word);
                            reading_word = false;
                            word = "";
                        }
                        if(isPunctuation(token[i]))
                        {
                            words.Add(word);
                            words.Add(token[i].ToString());
                            reading_word = false;
                            word = "";

                        }
                        if (Char.IsLetterOrDigit(token[i]) && !isPunctuation(token[i]))
                            word += token[i];
                    }

                }

                return words;
            }
            public List<string> wordsOnlyTokenizer(string token)
            {
                List<string> words = new List<string>();
                bool reading_word = false;
                string word = "";
                for (int i = 0; i < token.Length; i++)
                {
                    if (!reading_word)
                    {
                        if (Char.IsLetterOrDigit(token[i]))
                        {
                            reading_word = true;
                            word += token[i];
                        }
                    }
                    else // reading_word == true
                    {
                        if (i == (token.Length - 1))
                        {
                            if (!Char.IsLetterOrDigit(token[i]))
                                words.Add(word);
                            else if(Char.IsLetterOrDigit(token[i]))
                            {
                                word += token[i];
                                words.Add(word);
                            }
                            break;
                        }
                        if (!Char.IsLetterOrDigit(token[i]))
                        {
                            words.Add(word);
                            reading_word = false;
                            word = "";
                        }
                        if (Char.IsLetterOrDigit(token[i]))
                            word += token[i];
                    }

                }
                return words;
            }
            public List<string> whiteSpaceTokenizer(string token)
            {
                List<string> words = new List<string>();
                bool reading_word = false;
                string word = "";
                for (int i = 0; i < token.Length; i++)
                {
                    if (!reading_word)
                    {
                        if (token[i] != ' ' && token[i] != '\t')
                        {
                            reading_word = true;
                            word += token[i];
                        }
                    }
                    else // reading_word == true
                    {
                        if (i == (token.Length - 1))
                        {
                            if (token[i] == ' ')
                                words.Add(word);
                            else if (token[i] != ' ' && token[i] != '\t')
                            {
                                word += token[i];
                                words.Add(word);
                            }
                            break;
                        }
                        if (token[i] == ' ')
                        {
                            words.Add(word);
                            reading_word = false;
                            word = "";
                        }
                        if (token[i] != ' ' && token[i] != '\t')
                            word += token[i];
                    }

                }
                return words.Select(t => t.Trim()).ToList();
            }
        }
    }
    namespace Stopwords
    {
        public class StopWords
        {
            private List<string> stopWords;
            public StopWords()
            {
                stopWords = new List<string>()
                {
                    "to", "as", "go", "the", "of", "there", "hence", "example", 
                    "To", "As", "Go", "The", "Of", "There", "Hence", "Example",
                };
            }
            public List<string> getListOfStopWords()
            {
                return this.stopWords;
            }
            public List<string> removeWordsBasedOnStopWordsList(List<string> wordsList)
            {
                for (int i = wordsList.Count - 1; i >= 0; --i)
                {
                    var item = wordsList[i];
                    if (stopWords.Contains(item))
                        wordsList.Remove(item);
                }
                return wordsList;
            }
            public List<string> removeNumbers(List<string> wordsList)
            {
                for (int i = wordsList.Count - 1; i >= 0; --i)
                {
                    var item = wordsList[i];
                    int my_second_return;
                    bool success = int.TryParse(item, out my_second_return);
                    if(success)
                    {
                        wordsList.Remove(item);
                    }
                }
                return wordsList;
            }
            public List<string> removeSingleLetterWords(List<string> wordsList)
            {
                for (int i = wordsList.Count - 1; i >= 0; --i)
                {
                    var item = wordsList[i];
                    if (item.Length <= 1)
                        wordsList.Remove(item);
                }
                return wordsList;
            }
            public List<string> removeSpecificWord(List<string> wordsList, string word)
            {
                for (int i = wordsList.Count - 1; i >= 0; --i)
                {
                    var item = wordsList[i];
                    if (item == word)
                        wordsList.Remove(item);
                }
                return wordsList;
            }
            public List<string> removeInInterval(List<string> wordsList, string startWord, string endWord)
            {
                bool canDelete = false;
                for (int i = wordsList.Count - 1; i >= 0; --i)
                {
                    var item = wordsList[i];
                    if (!canDelete)
                    {
                        if (item == endWord)
                            canDelete = true;
                    }
                    if (canDelete)
                    {
                        wordsList.Remove(item);
                        if (item == startWord)
                            break;
                    }
                }
                return wordsList;
            }
        }
    }
    namespace Frequency
    {
        public class Frequnce
        {
            public Dictionary<string, int> getWordFrequence(List<string> elements)
            {
                var frequency = elements.GroupBy(x => x).OrderByDescending(x => x.Count()).ToDictionary(x => x.Key , x => x.Count());
                return frequency;
            }
        }

    }
    namespace Model
    {
        public class DataModel
        {
            private int dataLabelID;
            private int dataTagInfo;
            private string word;
            private float confidenceRate;
            public DataModel(int dataLabelID, int dataTagInfo, string word)
            {
                this.dataLabelID = dataLabelID;
                this.dataTagInfo = dataTagInfo;
                this.word = word;
            }
            public int getDataModelLabelID()
            {
                return this.dataLabelID;
            }
            public int getDataModelTagInfo()
            {
                return this.dataTagInfo;
            }
            public string getDataModelString()
            {
                return this.word;
            }
            public void setConfidenceProcentRate(float confidenceRate)
            {
                this.confidenceRate = confidenceRate;
            }
            public float getConfidenceProcentRate()
            {
                return this.confidenceRate;
            }
        }

        public class ModelTrainer
        {
            private List<DataModel> dmList;
            private int removingThresholdValue;
            public ModelTrainer()
            {
                dmList = new List<DataModel>();
            }

            public List<DataModel> getDataModelTrained()
            {
                return this.dmList;
            }

            public int getRemovingThresholdValue()
            {
                return this.removingThresholdValue;
            }

            public void setRemovingThresholdValue(int value)
            {
                this.removingThresholdValue = value;
            }

            public void loadData(int dataValueID, int addInfoData, string word)
            {
                dmList.Add(new DataModel(dataValueID, addInfoData, word));
            }

            public Dictionary<int, int> countValues()
            {
                var count = this.dmList.GroupBy(x => x.getDataModelLabelID()).ToDictionary(x => x.Key, x => x.Count());
                return count;
            }

            public void trainData()
            {
                int remFactor = this.getRemovingThresholdValue();
                removeItemFromList(remFactor);

                foreach (var item in dmList)
                {
                    int labelID = item.getDataModelLabelID();
                    float procent = 1.0f;
                    int count = 1;
                    foreach (var subitem in dmList) 
                        if(subitem.getDataModelLabelID() != labelID && item.getDataModelString() == subitem.getDataModelString())
                            count++;
                    procent /= (float)count;
                    item.setConfidenceProcentRate(procent);
                }

            }
            
            public void computeRemovingThresholdValue()
            {
                // less than factor of number removing
                var maxValCount = countValues().Values.Max();
                var minValCount = countValues().Values.Min();
                var countVals = countValues().Values.Count();
                float factor = ((float)(maxValCount / countVals) * (float)(minValCount / countVals)) / (float)(countVals * dmList.Count) * 0.50f;
                removingThresholdValue = (int)factor;
            }

            private void removeItemFromList(int factor)
            {
                for (int i = dmList.Count - 1; i >= 0; --i)
                {
                    var item = dmList[i];
                    if (item.getDataModelTagInfo() <= factor)
                        dmList.Remove(item);
                }
            }

        }

        public class Predictor
        {
            private Dictionary<int, float> predictorValues;
            private Dictionary<int, float> probabilities;
            public Predictor()
            {
                predictorValues = new Dictionary<int, float>();
                probabilities = new Dictionary<int, float>();
            }

            public void makePrediction(List<DataModel> trainedModel, List<string> words, int start_label)
            {
                // simplistic method
                int labelID = start_label;
                float prob = 0.0f;
                foreach (var item in trainedModel)
                {
                    if (item.getDataModelLabelID() != labelID)
                    {
                        predictorValues.Add(labelID, prob);
                        labelID = item.getDataModelLabelID();
                        prob = 0.0f;
                    }
                    if (item.getDataModelLabelID() == labelID && words.Contains(item.getDataModelString()))
                        prob += ((float)item.getDataModelTagInfo() * item.getConfidenceProcentRate() );
                    if (trainedModel.Last().Equals(item))
                        predictorValues.Add(labelID, prob);
                }

                calculateProbabilities();                
            }

            private void calculateProbabilities()
            {
                // simplistic probability calculation.
                float sum = predictorValues.Values.Sum();
                foreach (var item in predictorValues)
                    probabilities.Add(item.Key, (item.Value / sum));
            }

            public KeyValuePair<int, float> argmax()
            {
                var max = this.probabilities.OrderByDescending(x => x.Value).First();
                return max;
            }
        }
    }       
}
