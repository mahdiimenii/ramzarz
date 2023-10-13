using Newtonsoft.Json;
using ramzarz;


await RunInBackground(TimeSpan.FromSeconds(3), () => InitAsync());

async Task RunInBackground(TimeSpan timeSpan, Action action)
{
    var periodicTimer = new PeriodicTimer(timeSpan);
    while (await periodicTimer.WaitForNextTickAsync())
    {
        action();
    }
}



async Task InitAsync()
{
    HttpClient cl1 = new HttpClient();
    string strapi = "https://api.wallex.ir/v1/currencies/stats";

    HttpResponseMessage resp = await cl1.GetAsync(strapi);
    if (resp.IsSuccessStatusCode)
    {
        string apiresp = await resp.Content.ReadAsStringAsync();
        apitafsir apitafsir1 = JsonConvert.DeserializeObject<apitafsir>(apiresp);
        List<apietelaat> listdata1 = apitafsir1.result;

        foreach (var item in listdata1)
        {
            Console.WriteLine($"mokhafaf: {item.key}");
            Console.WriteLine($"esme kamel: {item.name_en}");
            Console.WriteLine($"qeymate hamin lahze: {item.price}");
            double pishbini = item.price + 0.0018;
            Console.WriteLine($"pishbiniye man az qeymate yek saate digiye {item.name_en}: {pishbini}");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
