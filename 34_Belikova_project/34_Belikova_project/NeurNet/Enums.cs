namespace _34_Belikova_project.NeurNet
{
   enum NeuronType // тип нейрона
    {
        Hidden, //скрытый слой
        Output //выходной слой
    }

    enum NetworkMode//режимы работы сети
    {
        Train, //обучение
        Test, //тест
        Recogn //распознование
    }

    enum MemoryMode//режимы работы памяти
    {
        GET, //считывание
        SET, //сохранение 
        INIT //инициализация
    }
}
