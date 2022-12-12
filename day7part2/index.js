// borrowed heavily (outright stolen) from Kent C Dodds:
// https://codesandbox.io/s/example-state-machine-implementation-vnz6c
// https://statecharts.dev
// https://kentcdodds.com/blog/implementing-a-simple-state-machine-library-in-javascript


const fs = require('fs');
const stateMachine = require('./fsm');
const f = fs.createReadStream('testInput');

const allLines = fs.readFileSync('testInput', 'utf-8').split('\n');
const directories = []
let currentDirectory = null;
const lsPattern = /\$\sls/g;
const directoryPattern = /dir\s(.*)/;
const directoryNamePattern = /cd\s(.*)/;
const filePattern = /(\d+)\s(.*)/;

const test = (input, pattern) => {
    return pattern.test(input);
}

const getDirectory = (input) => {

    const match = input.match(directoryPattern);
    const name = match[1];
    return { name: name, files: [], directories: [], parent: currentDirectory, size: 0 }

}

const getFile = (input) => {

    const match = input.match(filePattern);
    const size = parseInt(match[1]);
    const name = match[2];
    return { name: name, size: size };

}

const getDirectoryName = (input) => {
    const match = input.match(directoryNamePattern)

    if (match)
        return match[1];
}
handlers = {
    changeDirectory(line) {
        const directoryName = getDirectoryName(line)
        if (directoryName == '..') {
            if (currentDirectory.parent != undefined)
                currentDirectory = currentDirectory.parent;
        }
        else {
            currentDirectory = currentDirectory.directories.filter(d => d.name == directoryName)[0] || currentDirectory
        }
    },
    listFiles(line) {
        if (test(line, lsPattern))
            return;

        if (test(line, directoryPattern)) {
            const d = getDirectory(line);
            currentDirectory.directories.push(d)
            directories.push(d)
        }
        else if (test(line, filePattern))
            currentDirectory.files.push(getFile(line))
    }
}

let state = stateMachine.value
console.log(`current state: ${state}`) // current state: off
currentDirectory = { name: '/', files: [], directories: [], size: 0 };
directories.push(currentDirectory);
allLines.forEach(line => {

    console.log(`Processing ${line}`)
    if (line.match(/\$\scd/)) {
        stateMachine.transition(state, 'cd')
    }
    else if (line.match(/\$\sls/)) {
        stateMachine.transition(state, 'ls')
    }

    state = stateMachine.value
    handlers[state](line)
    console.log(`current state: ${state}`)
});


stateMachine.transition(state, 'done');

const printDirectory = (dir, level) => {
    console.log(`${'\t'.repeat(level)}${dir.name}`);
    dir.files.forEach(f => {
        console.log(`${'\t'.repeat(level)}\tfile: ${f.name}\t${f.size}`)
    });
    dir.directories.forEach(d => {
        printDirectory(d, level + 1)
    });
}

const getSize = (d) => {
    if (!d) return 0;
    let size = 0;
    d.files.forEach(f => {
        size += f.size
    });
    d.directories.forEach(d => {
        size += getSize(d)
    })
    return size;
}

const totalSpace = 70000000;
const neededSpaceForUpdate = 30000000;
const totalInUseSize = getSize(directories[0])
const unusedSpace = totalSpace - totalInUseSize;
const additionalNeededSpace = neededSpaceForUpdate - unusedSpace;

console.log(`Total Space:\t${totalSpace}`)
console.log(`In Use:\t${totalInUseSize}`)
console.log(`Total Needed:\t${additionalNeededSpace}`)

const deleteCandidate = []

directories.forEach(d => {
    d.size = getSize(d)
    if (d.size >= additionalNeededSpace)
        deleteCandidate.push(d)
});

const smallest = deleteCandidate.sort((a, b) => a.size - b.size)[0]
console.log(smallest)

// state = machine.transition(state, 'switch')
// console.log(`current state: ${state}`) // current state: on

// state = machine.transition(state, 'switch')
// console.log(`current state: ${state}`) // current state: off