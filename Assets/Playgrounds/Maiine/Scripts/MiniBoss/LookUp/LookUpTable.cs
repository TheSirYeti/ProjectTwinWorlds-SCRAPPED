using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpTable<T1, T2>
{
    //Creamos un delegate que tome un T1 como parametro,y devolvemos un T2,esto nos deja agregar cualquier funcion que tome un valor y devuelva otro
    public delegate T2 FactoryMethod(T1 keyToReturn);

    //Creamos un diccionario que use el tipo T1 como llave y T2 sea el valor que guarda.
    public Dictionary<T1, T2> _dic = new Dictionary<T1, T2>();

    //Creamos una variable del tipo FactoryMethod para guardar una referencia a la funcion que crea el valor,cuando no lo tenemos guardado en la Table
    FactoryMethod _factoryMethod;


    //Constructor en donde vamos a guardar la funcion que crea dicho valor si no lo tenemos,en la variable
    public LookUpTable(FactoryMethod method)
    {
        _factoryMethod = method;
    }

    //Funcion principal donde me pasan T1 para ver si esta en el diccionario,si lo tengo devuelvo el valor,si no es asi, lo creo,lo guardo y lo devuelvo
    public T2 ReturnValue(T1 key)
    {
        if (_dic.ContainsKey(key))
        {
            return _dic[key];
        }

        var value = _factoryMethod(key);
        _dic.Add(key, value);
        return _dic[key];
    }
}
