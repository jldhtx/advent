const fs = require('fs');
const CraneCommand = require('./craneCommand');
const CrateStacks = require('./crateStack');

const stacks = new CrateStacks(9);
stacks.add(1, 'Z')
stacks.add(1, 'P')
stacks.add(1, 'M')
stacks.add(1, 'H')
stacks.add(1, 'R')

stacks.add(2, 'P')
stacks.add(2, 'C')
stacks.add(2, 'J')
stacks.add(2, 'B')

stacks.add(3, 'S');
stacks.add(3, 'N');
stacks.add(3, 'H');
stacks.add(3, 'G');
stacks.add(3, 'L');
stacks.add(3, 'C');
stacks.add(3, 'D');

stacks.add(4, 'F');
stacks.add(4, 'T');
stacks.add(4, 'M');
stacks.add(4, 'D');
stacks.add(4, 'Q');
stacks.add(4, 'S');
stacks.add(4, 'R');
stacks.add(4, 'L');

stacks.add(5, 'F');
stacks.add(5, 'S');
stacks.add(5, 'P');
stacks.add(5, 'Q');
stacks.add(5, 'B');
stacks.add(5, 'T');
stacks.add(5, 'Z');
stacks.add(5, 'M');

stacks.add(6, 'T');
stacks.add(6, 'F');
stacks.add(6, 'S');
stacks.add(6, 'Z');
stacks.add(6, 'B');
stacks.add(6, 'G');

stacks.add(7, 'N');
stacks.add(7, 'R');
stacks.add(7, 'V');

stacks.add(8, 'P');
stacks.add(8, 'G');
stacks.add(8, 'L');
stacks.add(8, 'T');
stacks.add(8, 'D');
stacks.add(8, 'V');
stacks.add(8, 'C');
stacks.add(8, 'M');

stacks.add(9, 'W');
stacks.add(9, 'Q');
stacks.add(9, 'N');
stacks.add(9, 'J');
stacks.add(9, 'F');
stacks.add(9, 'M');
stacks.add(9, 'L');

console.log('Starting crates')
stacks.output()

const allFileContents = fs.readFileSync('testInput', 'utf-8');
allFileContents.split(/\r?\n/).forEach(line => {
    console.log(line);

    let command = new CraneCommand(line);
    stacks.move(command.from, command.to, command.move);
    stacks.output()
})


