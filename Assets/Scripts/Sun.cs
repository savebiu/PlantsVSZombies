using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ���� Sun �࣬�̳��� MonoBehaviour
public class Sun : MonoBehaviour
{
    // �ƶ�����ʱ��
    public float moveDuration = 1;
    // ����ֵ
    public int point = 50;

    // �����ƶ���Ŀ��λ�õķ���
    public void LinearTo(Vector3 targetPos)
    {
        transform.DOMove(targetPos, moveDuration); // ʹ�� DOTween �����ƶ�
    }

    // ��Ծ�ƶ���Ŀ��λ�õķ���
    public void JumpTo(Vector3 targetPos)
    {
        targetPos.z = -1; // ���� z ��λ��
        Vector3 centerPos = (transform.position + targetPos) / 2; // ��������λ��
        float distance = Vector3.Distance(transform.position, targetPos); // �������

        centerPos.y += (distance / 2); // ��������λ�õ� y ֵ

        // ʹ�� DOTween ����·������
        transform.DOPath(new Vector3[] { transform.position, centerPos, targetPos },
            moveDuration, PathType.CatmullRom).SetEase(Ease.OutQuad);
    }

    // ������¼�
    void OnMouseDown()
    {
        // �ƶ���̫�����ı�λ�ò�����ɺ�ִ�лص�
        transform.DOMove(SunManager.Instance.GetSunPointTextPosition(), moveDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(
            () =>
            {
                Destroy(this.gameObject); // ���ٵ�ǰ��Ϸ����
                SunManager.Instance.AddSun(point); // ���ӻ���
            }
            );
    }
}
