using System.Collections.Generic;

public class LocalizationManager
{
    private Dictionary<int, LocalizationData> localizationDataDict;

    public LocalizationManager()
    {
        // Здесь инициализируйте словарь с данными локализации для разных локалей.
        localizationDataDict = new Dictionary<int, LocalizationData>();

        // Пример инициализации для локалей 0 и 2 (русский и китайский).
        localizationDataDict[0] = new LocalizationData("ru");
        localizationDataDict[2] = new LocalizationData("zh");
        localizationDataDict[1] = new LocalizationData("en");
        // Добавьте другие локали по мере необходимости.
    }

    public LocalizationData GetLocalizationData(int localeId)
    {
        // Проверяем, есть ли данные для заданной локали в словаре.
        if (localizationDataDict.ContainsKey(localeId))
        {
            return localizationDataDict[localeId];
        }
        // Вернуть локаль по умолчанию, если данные отсутствуют.
        return localizationDataDict[0];
    }
}
