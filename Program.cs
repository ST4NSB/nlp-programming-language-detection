﻿using System;
using System.Collections.Generic;
using System.IO;

using NaturalLanguageProcessing.Tokenize;
using NaturalLanguageProcessing.Frequency;
using NaturalLanguageProcessing.Stopwords;
using NaturalLanguageProcessing.Model;


namespace nlp_prglang
{
    class FileReader
    {
        private string fileName;
        private readonly string pathFile;
        public FileReader(string fileName)
        {
            this.fileName = fileName;
            pathFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        }
        public List<string> getFileLines()
        {
            List<string> lines = new List<string>();
            if (File.Exists(pathFile))
            {
                foreach (string line in File.ReadLines(pathFile))
                    lines.Add(line);
            }
            else
            {
                throw new Exception("The file " + fileName + " doesn't exist in " + pathFile + "!");
            }
            return lines;
        }
        public string getAllTxt(bool output = false)
        {
            string text = "";
            if (File.Exists(pathFile))
            { 
                text = File.ReadAllText(pathFile);
                if (output)
                {
                    Console.WriteLine("+-------------------- BEGIN FILE -----------------------+");
                    Console.WriteLine(text);
                    Console.WriteLine("+--------------------- END FILE -----------------------+");
                    Console.WriteLine();
                }
            }
            else
                throw new Exception("The file " + fileName + " doesn't exist in " + pathFile + "!");
            return text;
        }
    }

    class Program
    {
        public const int LANG_CPP = 0;
        public const int LANG_JAVA = 1;

        static Dictionary<string, int> loadAndProcessData(string file)
        {
            // load training files from directory
            string learn = new FileReader(file).getAllTxt();

            // tokenize words (words only method here)
            List<string> dataTokenized = new Token().wordsOnlyTokenizer(learn);

            // removing numbers & removing single letter character & based on stoplist
            StopWords sw = new StopWords();
            dataTokenized = sw.removeNumbers(dataTokenized);
            dataTokenized = sw.removeSingleLetterWords(dataTokenized);
            dataTokenized = sw.removeWordsBasedOnStopWordsList(dataTokenized);

            // get frequency per word 
            var data = new Frequnce().getWordFrequence(dataTokenized);

            return data;
        }

        /*static void debugFunc(ModelTrainer mt, Predictor predictor)
        {
            Console.WriteLine("-------- Model Data List ------- ..");
            foreach (var item in mt.getDataModelTrained())
                Console.WriteLine("Label-ID: " + item.getDataModelLabelID() + "|| Word-Frequency: " + item.getDataModelTagInfo() +
                   "|| Word: " + item.getDataModelString() + "|| Trust Probability: " + item.getTrustProcent());
            Console.WriteLine("-------------------------------- .. ");

            Console.WriteLine("----- Threshold of removing the frequency of words ------ ..");
            Console.WriteLine(mt.getRemovingThresholdValue());
            Console.WriteLine("--------------------- .. ");

            Console.WriteLine("------ Values count of every Label-ID ------- .. ");
            foreach (var item in mt.countValues())
                Console.WriteLine(item.Key + " => " + item.Value);
            Console.WriteLine("--------------------- .. ");

            Console.WriteLine("------ Prediction values (0.f) -------- ..");
            foreach (var item in predictor.predictorValues)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }
            Console.WriteLine("-------------------- .. ");

            Console.WriteLine("------ Prediction probabilities (%) -------- ..");
            foreach (var item in predictor.probabilities)
            {
                Console.WriteLine(item.Key + " -> " + item.Value);
            }
            Console.WriteLine("-------------------- .. ");
        }*/

        static void Main(string[] args)
        {
            // load and process data from the files
            Dictionary<string, int> dataCpp = loadAndProcessData("files/training_cpp.txt");
            Dictionary<string, int> dataJava = loadAndProcessData("files/training_java.txt");

            // create model
            ModelTrainer mt = new ModelTrainer();
            
            // load processed data in model
            foreach (var item in dataCpp)
                mt.loadData(LANG_CPP, item.Value, item.Key);
            foreach (var item in dataJava)
                mt.loadData(LANG_JAVA, item.Value, item.Key);

            mt.computeRemovingThresholdValue();
            // mt.setRemovingThresholdValue(0);
            mt.trainData();

            // load 
            string pred = new FileReader("files/predict.txt").getAllTxt(true);
            List<string> predProcessed = new Token().wordsOnlyTokenizer(pred);
            predProcessed = new StopWords().removeWordsBasedOnStopWordsList(predProcessed);
      
            // calculate the values and create probabilities based on labels
            Predictor predictor = new Predictor();
            var mymodel = mt.getDataModelTrained();
            predictor.simplisticPrediction(mymodel, predProcessed, LANG_CPP);
            var max = predictor.argmax();

            //debugFunc(mt, predictor);

            string msg = "The model predicts: ";
            switch(max.Key)
            {
                case LANG_CPP: msg += "C++"; break;
                case LANG_JAVA: msg += "Java"; break;
            }
            msg += " as the language of the file, with " + max.Value + " trust procent! (" + (0.999999f - max.Value) + " for Java)";
            Console.WriteLine(msg);

        }
    }
}
