using UnityEngine;
using UnityEditor;
using static ShipShooting;

[CustomEditor(typeof(ShipShooting))]
public class ShipShootingEditor : Editor
{
    private int bulletSpawnerCount = 0;
    public override void OnInspectorGUI()
    {
        ShipShooting shipShooting = (ShipShooting)target;

        base.OnInspectorGUI();
        return;

        EditorGUILayout.IntField("BulletSpawnerCount", bulletSpawnerCount);
        //shipShooting.bulletSpawners = new BulletSpawner[bulletSpawnerCount];

        foreach (BulletSpawner bulletSpawner in shipShooting.bulletSpawners)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.Vector2Field("Direction", bulletSpawner.shotDirection);
            GUILayout.EndHorizontal();
        }
    }
}
