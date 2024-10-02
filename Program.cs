using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


            HttpClient client = new HttpClient();
            string s = await client.GetStringAsync("https://coderbyte.com/api/challenges/json/json-cleaning");
            Console.WriteLine(s);            
            //---------------------------------------------------------------------------------------------------------

            //Start traversing
            Dictionary<string,object> res = new Dictionary<string, object>();
            var details = JObject.Parse(s);
            foreach (KeyValuePair<String, JToken> app in details)
            {
                var appName = app.Key;
                var count = app.Value.Count();
                var type = app.Value.Type;
                if(type == JTokenType.Array){                                        
                    List<string> strings = new List<string>();                    
                    for(int i=0;i<app.Value.Count();i++)
                    {
                        string item = app.Value[i].ToString();
                        if(item!="N/A" && item!="-" && item.Trim() !=string.Empty)                            
                            strings.Add(item);                            
                    }
                    res.Add(app.Key,strings);
                }
                else if(type== JTokenType.Object){
                    var arrStr = app.Value;                                        
                    JObject jb = new JObject();                    
                    foreach (JProperty  x in arrStr)
                    {
                            string name = x.Name;
                            JToken value = x.Value;

                      if(value.ToString()!="N/A" && value.ToString()!="-" && value.ToString().Trim() !=string.Empty)
                            jb.Add(name,value);    
                    }
                    res.Add(app.Key, jb);
                }
                else if(type == JTokenType.String) {
                    if(app.Value.ToString()!="N/A" && app.Value.ToString()!="-" && app.Value.ToString().Trim() !=string.Empty)
                    res.Add(app.Key,app.Value);                 
                }
                else {
                    res.Add(app.Key,app.Value);
                }                                            
            }

            //Final conversion to JSON:
            string finalObject = JsonConvert.SerializeObject(res);
            Console.WriteLine("\n Final Object");
            Console.WriteLine(finalObject);

            //References:
            /*
            1- https://stackoverflow.com/questions/12676746/parse-json-string-in-c-sharp
            2- https://forum.uipath.com/t/how-to-check-if-jtoken-is-null/274329
            3- https://code-maze.com/csharp-how-to-serialize-a-dictionary-to-json/
            4- https://stackoverflow.com/questions/10543512/how-do-i-enumerate-through-a-jobject
            */





