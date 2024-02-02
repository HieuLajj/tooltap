using System.Collections;
using UnityEngine;

public class Cubifier : MonoBehaviour
{
    public GameObject PrefabCube;

    public GameObject PrefabCubeMini;

    public GameObject TargetCube;
    private GameObject TargetCube2;

    public Vector3 SectionCount;
//    public Material SubCubeMaterial;

    private Vector3 SizeOfOriginalCube;
    private Vector3 SectionSize;
    private Vector3 FillStartPosition;
    private Transform ParentTransform;
    private GameObject SubCube;

    private MeshRenderer vfxRenderer;
    private void Awake()
    {
        vfxRenderer = TargetCube.GetComponent<MeshRenderer>();
    }
    void Start()
    {
        Controller.Instance.blockMiniList.Clear();
        Controller.Instance.blockMiniDictionary.Clear();

        TargetCube2 = Instantiate(PrefabCube, TargetCube.transform.position,Quaternion.identity);

       
        TargetCube2.transform.localScale = vfxRenderer.bounds.size;
        if (TargetCube2 == null)
            TargetCube2 = gameObject;

        SizeOfOriginalCube = TargetCube2.transform.lossyScale;
        SectionSize = new Vector3(
            SizeOfOriginalCube.x / SectionCount.x,
            SizeOfOriginalCube.y / SectionCount.y,
            SizeOfOriginalCube.z / SectionCount.z
            );

        FillStartPosition = TargetCube2.transform.TransformPoint(new Vector3(-0.5f, 0.5f, -0.5f))
                            + TargetCube2.transform.TransformDirection(new Vector3(SectionSize.x, -SectionSize.y, SectionSize.z) / 2.0f);
        ParentTransform = new GameObject(TargetCube2.name + "CubeParent").transform;
        Controller.Instance.pretransform = ParentTransform;
        DivideIntoCuboids();
    }




    private void DivideIntoCuboids()
    {
        for (int i = 0; i < SectionCount.x; i++)
        {
            for (int j = 0; j < SectionCount.y; j++)
            {
                for (int k = 0; k < SectionCount.z; k++)
                {
                    SubCube = Instantiate(PrefabCubeMini);
                    
                    SubCube.transform.localScale = SectionSize;
                    SubCube.transform.position = FillStartPosition + TargetCube2.transform.TransformDirection(new Vector3((SectionSize.x) * i, -(SectionSize.y) * j, (SectionSize.z) * k));
                    SubCube.transform.rotation = TargetCube2.transform.rotation;

                    SubCube.transform.SetParent(ParentTransform);
                    // SubCube.GetComponent<MeshRenderer>().material = SubCubeMaterial;
                    BlockMini blockMini = SubCube.GetComponent<BlockMini>();
                    Controller.Instance.blockMiniList.Add(blockMini);
                    string key = blockMini.transform.position.x + "|" + blockMini.transform.position.y + "|" + blockMini.transform.position.z;
                    Controller.Instance.blockMiniDictionary.Add(key,blockMini);
                }
            }
        }
        Destroy(TargetCube2);
    }

}