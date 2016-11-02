using UnityEngine;
using System.Collections;
using System;
//TODO Check C# docs if there's a foundation class for this
public class FamiliarController : MonoBehaviour {
    private Node head;
    public GameObject player;
    public GameObject shot;

    private int nodeCount;
    public GameObject spawn; // For want of a better name;
    private int familiarCount;
    private GameObject[] familiars;
    // Use this for initialization
    void Start() {
        head = createNewNode();
        Node currentNode = head;
        for (int x = 0; x < 100; x++)
        {
            currentNode.next = createNewNode();
            currentNode = currentNode.next;
        }

        familiarCount = 0;
        familiars = new GameObject[4];

    }

    class Node
    {
        public Node next;
        public Vector3 position;
    }
    public void addFamiliar()
    {
        if (familiarCount>=4)
        {
            return;
        }
        GameObject newFamiliar = (GameObject)Instantiate(spawn, player.transform);
        familiars[familiarCount++] = newFamiliar;
    }

    internal void fire()
    {
        for (int x = 0; x < familiarCount; x++) {
            Instantiate(shot, familiars[x].transform.position, Quaternion.identity);
        }
    }

    public void updatePosition()
    {
        Node newNode = new Node();
        newNode.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        newNode.next = head;
        head = newNode;
        Node currentNode = head;
        Node previousNode = null;
        //TODO use tailNode instead; avoid iterating all of them every time we move
        while (currentNode.next != null)
        {
            previousNode = currentNode;
            currentNode = currentNode.next;
        }
        currentNode = null;
        if (previousNode != null)
        {
            previousNode.next = null;
        }
        nodeCount--;

    }
    private Node createNewNode()
    {
        Node newNode = new Node();
        nodeCount++;
        newNode.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        return newNode;
    }
    void Update()
    {
        Node currentNode = head;
        for (int x = 0; x < familiarCount; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                currentNode = currentNode.next;
            }
            familiars[x].transform.position
                = currentNode.position;
        }
    }

}
