using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Orazum.Utilities;

public class SideIndexer : MonoBehaviour
{
    [SerializeField]
    private Vector3 _spawnPos;

    [SerializeField]
    private float _lerpSpeed;

    [SerializeField]
    private Transform _symbolsParent;

    [SerializeField]
    private DiceSymbol[] _diceSymbolPrefabs;

    private void Awake()
    {
        InputDelegatesContainer.SideSymbolPlacement += OnSideSymbolPlacement;
    }

    private void OnDestroy()
    { 
        InputDelegatesContainer.SideSymbolPlacement -= OnSideSymbolPlacement;
    }

    private void OnSideSymbolPlacement(int dotCount)
    {
        int prefabIndex = dotCount - 1;
        StartCoroutine(SpawnSequence(prefabIndex));
    }

    private IEnumerator SpawnSequence(int prefabIndex)
    { 
        Vector3 targetPos = GameDelegatesContainer.FuncCurrentSidePos();
        DiceSymbol symbolPrefab = Instantiate(_diceSymbolPrefabs[prefabIndex]);

        symbolPrefab.transform.SetParent(_symbolsParent, true);
        Transform toMove = symbolPrefab.transform;
        toMove.position = _spawnPos;
        toMove.rotation = Quaternion.LookRotation((targetPos - _spawnPos).normalized, Vector3.up);
        float lerpParam = 0;
        while (lerpParam < 1)
        {
            lerpParam += _lerpSpeed * Time.deltaTime;
            toMove.position = Vector3.Lerp(_spawnPos, targetPos, MathUtilities.EaseInOut(lerpParam));
            yield return null;
        }
    }
}
