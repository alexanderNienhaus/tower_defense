using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestSuite
{
    [Test]
    public void TestSuiteSimplePasses()
    {
    }

    [UnityTest]
    public IEnumerator WaveSpawningTest()
    { 
        SceneManager.LoadScene("WaveTestQuick");
        yield return new WaitForSeconds(2f);
        Scene activeScene = SceneManager.GetActiveScene();
        GameObject waveManager = GameObject.FindGameObjectWithTag("WaveManager");
        WaveController waveController = waveManager.GetComponent<WaveController>();
        waveController.StartSpawning();
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Enemy").Length == 10);
        SceneManager.UnloadSceneAsync(activeScene);
    }

    [UnityTest]
    public IEnumerator PathfindingTest()
    {
        SceneManager.LoadScene("PathfinindingTestQuick");
        yield return new WaitForSeconds(2f);
        Scene activeScene = SceneManager.GetActiveScene();
        yield return new WaitForSeconds(2f);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        SceneManager.UnloadSceneAsync(activeScene);
    }

    [UnityTest]
    public IEnumerator SingleTargetTowerTest()
    {
        Vector3 posToAttack = new(-4, 0, 1);
        SceneManager.LoadScene("SingleTargetTowerTestQuick");
        yield return new WaitForSeconds(3f);
        Scene activeScene = SceneManager.GetActiveScene();
        EnemyController enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemy.transform.position = posToAttack;
        EnemySpawnsEvent enemySpawnsEvent = new EnemySpawnsEvent();
        enemySpawnsEvent.Raise(enemy);
        yield return new WaitForSeconds(3f);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        SceneManager.UnloadSceneAsync(activeScene);
    }

    [UnityTest]
    public IEnumerator AoeTowerTest()
    {
        Vector3 posToAttack = new(-4, 0, 1);
        SceneManager.LoadScene("AoeTowerTestQuick");
        yield return new WaitForSeconds(2f);
        Scene activeScene = SceneManager.GetActiveScene();
        EnemyController enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemy.transform.position = posToAttack;
        EnemySpawnsEvent enemySpawnsEvent = new EnemySpawnsEvent();
        enemySpawnsEvent.Raise(enemy);
        yield return new WaitForSeconds(10f);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        SceneManager.UnloadSceneAsync(activeScene);
    }

    [UnityTest]
    public IEnumerator DebuffTowerTest()
    {
        Vector3 posToAttack = new(-4, 0, 1);
        SceneManager.LoadScene("DebuffTowerTestQuick");
        yield return new WaitForSeconds(2f);
        Scene activeScene = SceneManager.GetActiveScene();
        EnemyController enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        enemy.transform.position = posToAttack;
        float initialSpeed = enemy.GetSpeed();
        EnemySpawnsEvent enemySpawnsEvent = new EnemySpawnsEvent();
        enemySpawnsEvent.Raise(enemy);
        yield return new WaitForSeconds(3f);
        Assert.Less(enemy.GetSpeed(), initialSpeed);
        SceneManager.UnloadSceneAsync(activeScene);
    }
}
