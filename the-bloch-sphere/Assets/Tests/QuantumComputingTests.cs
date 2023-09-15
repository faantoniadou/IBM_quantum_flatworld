using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UIManagerTests
{
    private UIManager uiManager;
    private QuantumGateHandler quantumGateHandlerMock;

    [SetUp]
    public void SetUp()
    {
        // Assuming prefabs are set up with necessary components and objects.
        GameObject go = new GameObject();
        uiManager = go.AddComponent<UIManager>();
        quantumGateHandlerMock = go.AddComponent<QuantumGateHandler>();


        uiManager.quantumGateHandler = quantumGateHandlerMock;
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up GameObject and Components created in setup
        //Object.Destroy(uiManager.gameObject);
    }

    [Test]
    public void TestShowBlochSpherePanel()
    {
        uiManager.ShowBlochSpherePanel();

        Assert.IsFalse(uiManager.welcomePanel.activeSelf);
        Assert.IsTrue(uiManager.blochSpherePanel.activeSelf);
    }

    [Test]
    public void TestShowQuboPanel()
    {
        uiManager.ShowQuboPanel();

        Assert.IsFalse(uiManager.blochSpherePanel.activeSelf);
        Assert.IsTrue(uiManager.quboPanel.activeSelf);
    }

    [Test]
    public void TestOnGateHover_Hadamard()
    {
        uiManager.OnGateHover("hadamard");

        Assert.IsTrue(uiManager.gateExplainerPanel.activeSelf);
        Assert.AreEqual("The Hadamard gate creates superposition and is a 180 degree rotation around the diagonal X+Z axis.", uiManager.gateExplainerText.text);
    }

    // ... (similar tests for other gate hovers) ...

    // Additional tests could include checking keyboard input handling, etc.
}
