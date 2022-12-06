const craneCommand = require('./craneCommand');

test('constructor properly initializes', () => {
    let command = new craneCommand('move 3 from 1 to 2')

    expect(command.from).toBe(1);
    expect(command.to).toBe(2);
    expect(command.move).toBe(3);
})

