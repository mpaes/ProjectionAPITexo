using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetcAPITexo.Business
{
    public class CSVBusiness : IFileBusiness
    {
        public Root ReadCSV<MovieList>(Stream file)
        {

            var listRetorn = new List<MovieListRetorn>();

            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", HeaderValidated = null, Encoding = Encoding.UTF8 });
            var records = csv.GetRecords<MovieList>().ToList();
            if (records.Count == 0)
            {
                throw new InvalidOperationException("File empty");
            }
            if (ValidHeader(csv.HeaderRecord) == false)
                throw new InvalidOperationException("Header inválid");

            var converted = JsonConvert.SerializeObject(records);
            var convertedContent = JArray.Parse(converted);
            var convertedContentPos = convertedContent.Where(item => item["winner"].ToString().ToUpper() == "YES").OrderBy(item => item["producers"]).ThenBy(x => x["year"]).ToList();

            // Atualizar uma terceira lista (unificar dados)
            foreach (var recordsList in convertedContentPos)
            {
                var listRetornUpdate = new MovieListRetorn();
                var found = (listRetorn.Where(item => item.producer == recordsList["producers"].ToString()).FirstOrDefault() != null) ? 1 : -1;
                if (found == -1)
                {
                    listRetornUpdate.producer = recordsList["producers"].ToString();
                    listRetornUpdate.previousWin = (int)recordsList["year"];
                    listRetorn.Add(listRetornUpdate);
                }
                else
                {
                    if (listRetorn.Any(item => item.producer == recordsList["producers"].ToString()))
                    {
                        var item = listRetorn.Find(x => x.producer == recordsList["producers"].ToString() && x.previousWin != null && x.followingWin == null);

                        var findDadosLastEmpty = (listRetorn.Where(items => items.producer == recordsList["producers"].ToString() && items.followingWin == null).FirstOrDefault() != null) ? 1 : -1;
                        if (findDadosLastEmpty == -1)
                        {
                            listRetornUpdate.producer = recordsList["producers"].ToString();
                            listRetornUpdate.previousWin = (int)recordsList["year"];
                            listRetorn.Add(listRetornUpdate);
                        }
                        else
                        {
                            item.followingWin = (int)recordsList["year"];
                            item.interval = item.followingWin - item.previousWin;
                        }
                    }
                }
            }

            return PrepareReturn(listRetorn);
        }

        private bool ValidHeader(string[] header)
        {
            var Header = new List<string>();
            Header.Add("year");
            Header.Add("studios");
            Header.Add("producers");
            Header.Add("winner");
            Header.Add("title");
            Header.Add("");

            foreach (var item in header)
            {
                if (Header.IndexOf(item.ToString()) < 0)
                    return false;
            }

            return true;
        }

        private Root PrepareReturn(List<MovieListRetorn> ListReturn)
        {
            Root root = new Root()
            {
                min = new List<Min>(),
                max = new List<Max>(),
            };
            var listMax = new List<Max>();
            var listMin = new List<Min>();


            //Identificar o menor emaior intervalo da base
            int maxInterval = (int)ListReturn.Max(t => t.interval);
            int minInterval = (int)ListReturn.Min(t => t.interval);

            // Obter o produtor com maior intervalo entre dois prêmios consecutivos
            var filteredListMax = ListReturn.Where(item => item.interval == maxInterval).ToList();
            foreach (var item in filteredListMax)
            {
                var max = new Max();
                max.interval = item.interval;
                max.producer = item.producer;
                max.followingWin = item.followingWin;
                max.previousWin = item.previousWin;
                listMax.Add(max);
            }

            var filteredListMin = ListReturn.Where(item => item.interval == minInterval).ToList();
            foreach (var item in filteredListMin)
            {
                var mim = new Min();
                mim.interval = item.interval;
                mim.producer = item.producer;
                mim.followingWin = item.followingWin;
                mim.previousWin = item.previousWin;
                listMin.Add(mim);
            }

            root.min = listMin;
            root.max = listMax;

            return root;
        }
    }
}
