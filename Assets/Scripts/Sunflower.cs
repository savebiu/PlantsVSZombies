
using UnityEngine;

public class Sunflower : Plant
{
    // ��������ĳ���ʱ��
    public float produceDuration = 5;
    private float produceTimer = 0; // ��ʱ��
    private Animator anim; // ����������
    public GameObject sunPrefab; // ����Ԥ����

    // ��Ծ����С��������
    public float jumpMinDistance = 0.3f;
    public float jumpMaxDistance = 2;

    private void Awake()
    {
        // ��ȡ�������
        anim = GetComponent<Animator>();
    }

    protected override void EnableUpdate()
    {
        // ���¼�ʱ��
        produceTimer += Time.deltaTime;

        // �����ʱ��������������ʱ�䣬�������⶯��
        if (produceTimer > produceDuration)
        {
            produceTimer = 0; // ���ü�ʱ��
            anim.SetTrigger("IsGlowing"); // �������⶯��
        }
    }

    // ��������ķ���
    public void ProduceSun()
    {
        // ʵ��������Ԥ����
        GameObject go = GameObject.Instantiate(sunPrefab, transform.position, Quaternion.identity);

        // ���������Ծ����
        float distance = Random.Range(jumpMinDistance, jumpMaxDistance);
        distance = Random.Range(0, 2) < 1 ? -distance : distance; // ���ѡ����
        Vector3 positon = transform.position; // ��ȡ��ǰֲ��λ��
        positon.x += distance; // ��������λ��

        // ��������Ծ��ָ��λ��
        go.GetComponent<Sun>().JumpTo(positon);
    }
}
