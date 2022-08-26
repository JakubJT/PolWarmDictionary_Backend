using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class DbInitializer
    {
        public static void Initialize(DictionaryContext context)
        {
            if (context.Words.Any())
            {
                return;
            }

            var kuba = new Author
            {
                Name = "Kuba"
            };
            var innyKuba = new Author
            {
                Name = "Innykuba"
            };
            context.AddRange(kuba, innyKuba);
            context.SaveChanges();

            var noun = new PartOfSpeech
            {
                Name = "Noun"
            };
            var liczebnik = new PartOfSpeech
            {
                Name = "Liczebnik"
            };
            context.AddRange(noun, liczebnik);
            context.SaveChanges();

            var stryjna = new Word
            {
                InWarmian = "Stryjna",
                InPolish = "Żona styja",
                Author = kuba,
                PartOfSpeech = noun
            };

            var czlek = new Word
            {
                InWarmian = "Człek",
                InPolish = "Człowiek",
                Author = kuba,
                PartOfSpeech = noun
            };

            var grubasa = new Word
            {
                InWarmian = "Grubasa",
                InPolish = "Grubas",
                Author = kuba,
                PartOfSpeech = noun
            };

            var ruchelka = new Word
            {
                InWarmian = "Ruchełka",
                InPolish = "Bukiet",
                Author = innyKuba,
                PartOfSpeech = noun
            };
            var chojina = new Word
            {
                InWarmian = "Chojina",
                InPolish = "Sosna",
                Author = innyKuba,
                PartOfSpeech = noun
            };

            var jerzba = new Word
            {
                InWarmian = "Jerzba",
                InPolish = "Wierzba",
                Author = kuba,
                PartOfSpeech = noun
            };
            var kruszka = new Word
            {
                InWarmian = "Kruszka",
                InPolish = "Grusza",
                Author = innyKuba,
                PartOfSpeech = noun
            };

            var sztery = new Word
            {
                InWarmian = "Sztery",
                InPolish = "Cztery",
                Author = kuba
            };
            context.AddRange(kruszka, jerzba, chojina, ruchelka, czlek, stryjna, grubasa);
            context.SaveChanges();



            var drzewa = new WordGroup
            {
                Name = "Drzewa",
                Words = new List<Word>() { kruszka, jerzba, chojina }
            };
            var rodzina = new WordGroup
            {
                Name = "Rodzina",
                Words = new List<Word>() { stryjna }
            };
            context.AddRange(drzewa, rodzina);

            context.SaveChanges();
        }
    }
}