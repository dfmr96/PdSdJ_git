using System;
using System.Collections.Generic;
using Command;
using UnityEngine;

namespace EventQueue
{
    public class EventQueue : MonoBehaviour
    {
        private List<ICommand> currentCommands = new();
        private Stack<IDeletableCommand> undoableCommands = new();
        public static EventQueue Instance { get; private set; }
        private static EventQueue _instance;

        public static EventQueue GetInstance()
        {
            //Does it exist? Return. Otherwise, create
            return _instance;
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void EnqueueCommand(ICommand command)
        {
            currentCommands.Add(command);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.Z))
            {
                UndoLatest();
            }
        }

        private void LateUpdate()
        {
            if (currentCommands.Count == 0)
                return;

            foreach (var command in currentCommands)
            {
                command.Execute();
                if (command is IDeletableCommand undoableCommand)
                {
                    undoableCommands.Push(undoableCommand);
                }
            }

            currentCommands.Clear();
        }

        public void UndoLatest()
        {
            if (undoableCommands.Count == 0)
            {
                return;
            }

            IDeletableCommand command = undoableCommands.Pop();
            command.Undo();
        }

        public void RemoveAllCommandOfType<T>() where T : IDeletableCommand
        {
            Stack<IDeletableCommand> tempStack = new Stack<IDeletableCommand>();

            while (undoableCommands.Count > 0)
            {
                IDeletableCommand command = undoableCommands.Pop();
                if (command is not T)
                {
                    tempStack.Push(command);
                }
                else
                {
                    command.Undo();
                    Debug.Log($"{command.GetType()} ha sido borrado");
                }
            }

            while (tempStack.Count > 0)
            {
                undoableCommands.Push(tempStack.Pop());
            }
        }
    } 
}
