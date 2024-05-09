using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : MonoBehaviour
{
    [SerializeField] private Node _startNode;
    [SerializeField] private Node _endNode;
    [SerializeField] private Product _transferredProduct;
    private float _transferTime;
    private LineRenderer _lineRenderer;

    private float _elapsedTime = 0;

    private void Awake() {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start() {
        _lineRenderer.SetPosition(0, _startNode.transform.position);
        _lineRenderer.SetPosition(1, _endNode.transform.position);

        _transferTime = Vector3.Distance(_startNode.transform.position, _endNode.transform.position);
    }

    private void Update() {
        _transferredProduct.transform.position = Vector3.Lerp(_startNode.transform.position, _endNode.transform.position, _elapsedTime / _transferTime);
        //Gradient gradient = _lineRenderer.colorGradient;
        //gradient.mode = GradientMode.Blend;

        //var colors = new GradientColorKey[2];
        //colors[0] = new GradientColorKey(Color.green, _elapsedTime / _transferTime);
        //colors[1] = new GradientColorKey(Color.red, 1f);

        //var alphas = new GradientAlphaKey[2];
        //alphas[0] = new GradientAlphaKey(1f, _elapsedTime / _transferTime);
        //alphas[1] = new GradientAlphaKey(1f, 1f);

        //gradient.SetKeys(colors, alphas);

        //_lineRenderer.colorGradient = gradient;
        _elapsedTime += Time.deltaTime;
    }
}
