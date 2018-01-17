using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private List<int> pinsKnockedDown;

    [SetUp]
    public void Setup()
    {
        pinsKnockedDown = new List<int>();
    }


    [Test]
    public void T00_PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_FirstStrikeReturnsEndTurn()
    {
        pinsKnockedDown.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        pinsKnockedDown.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T03_2ndBowlReturnsEndTurn()
    {
        pinsKnockedDown.Add(1);
        pinsKnockedDown.Add(1);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T04_Strike1InLastFrame()
    {
        for (int i = 0; i < 10; i++)
        {
            pinsKnockedDown.Add(10);
        }

        Assert.AreEqual(reset, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T05_Strike2InLastFrame()
    {
        for (int i = 0; i < 11; i++)
        {
            pinsKnockedDown.Add(10);
        }
        Assert.AreEqual(reset, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T06_Strike3InLastFrame()
    {
        for (int i = 0; i < 12; i++)
        {
            pinsKnockedDown.Add(10);
        }
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T07_Open1InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            pinsKnockedDown.Add(10);
        }
        pinsKnockedDown.Add(1);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T08_Spare1and2InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            pinsKnockedDown.Add(10);
        }
        pinsKnockedDown.Add(1);
        pinsKnockedDown.Add(9);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T09_Open1and2InLastFrame()
    {
        for (int i = 0; i < 9; i++)
        {
            pinsKnockedDown.Add(10);
        }
        pinsKnockedDown.Add(1);
        pinsKnockedDown.Add(1);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T10_Strike1Open2InLastFrame()
    {
        for (int i = 0; i < 10; i++)
        {
            pinsKnockedDown.Add(10);
        }
        pinsKnockedDown.Add(9);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T11_Strike1Open2Spare3InLastFrame()
    {
        for (int i = 0; i < 10; i++)
        {
            pinsKnockedDown.Add(10);
        }
        pinsKnockedDown.Add(1);
        pinsKnockedDown.Add(9);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinsKnockedDown));
    }

    [Test]
    public void T12_0and10Spareand1to9NextFrameReturnsTidy()
    {
        pinsKnockedDown.Add(0);
        pinsKnockedDown.Add(10);
        pinsKnockedDown.Add(9);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinsKnockedDown));
    }
}
