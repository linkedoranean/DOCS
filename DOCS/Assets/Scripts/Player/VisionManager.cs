using UnityEngine;

public class VisionManager : MonoBehaviour
{
    //TODO nesse script precisa atirar raycast em todas as direções para saber se pode movimentar ao redor
    //por enquanto o script só verifica se pode andar pra frente

    private LayerMask layerMask;
    [SerializeField] private float hitDistance;
    
    void Awake()
    {
        layerMask = LayerMask.GetMask("FloorTile");
        CharactersManager.OnMoved += CastRaycast;
    }

    void OnDestroy()
    {
        CharactersManager.OnMoved -= CastRaycast;
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.green);
    }

    void CastRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, hitDistance, layerMask))
        {
            hit.transform.GetComponent<TileManager>().DisplayTile();
            //Debug.LogError(hit.transform.name);
        }
    }
}