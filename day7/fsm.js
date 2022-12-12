const stateMachine = createMachine({
    initialState: 'awaitingCommand',
    awaitingCommand: {
        actions: {
            onEnter() { console.log('awaiting command...') },
            onExit() { console.log('processing command') },
        },
        transitions: {
            ls: {
                target: 'listFiles',
                action() { }
            },
            cd: {
                target: 'changeDirectory',
                action() { }
            }
        }
    },
    listFiles: {
        actions: {
            onEnter() { },
            onExit() { },
        },
        transitions: {
            cd: {
                target: 'changeDirectory',
                action() { }
            },
            done: {
                target: 'done',
                action() { }
            }
        }
    },
    changeDirectory: {
        actions: {
            onEnter() { },
            onExit() { },
        },
        transitions: {
            ls: {
                target: 'listFiles',
                action() { }
            },
            done: {
                target: 'done',
                action() { console.log('done') }
            },
            cd: {
                target: 'changeDirectory',
                action() { }
            }
        }
    },
    done: {
        actions: {
            onEnter() { console.log('done') }
        }
    }
})

function createMachine(definition) {
    const machine = {
        value: definition.initialState,
        transition(currentState, event) {
            const currentStateDefinition = definition[currentState]
            const destinationTransition = currentStateDefinition.transitions[event]
            if (!destinationTransition) {
                return
            }
            const destinationState = destinationTransition.target
            const destinationStateDefinition =
                definition[destinationState]

            destinationTransition.action()
            currentStateDefinition.actions.onExit()
            destinationStateDefinition.actions.onEnter()

            machine.value = destinationState

            return machine.value
        }
    };

    return machine;
}

module.exports = stateMachine;