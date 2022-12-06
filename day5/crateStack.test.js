const stacks = require('./crateStack');

test('constructor properly initializes', () => {
    let s = new stacks(10);
    expect(s.stacks.length).toEqual(10);
});

test('add a crate to stack 5', () => {
    let s = new stacks(10);
    s.add(5, 'c');
    expect(s.stacks.length).toEqual(10);
    expect(s.stacks[4].length).toEqual(1);
    expect(s.stacks[4][0]).toBe('c');
});

test('move a crate', () => {
    let s = new stacks(10);
    s.stacks[0].push('c')
    s.move(1, 2, 1);

    expect(s.stacks[0].length).toEqual(0);
    expect(s.stacks[1].length).toEqual(1);
    expect(s.stacks[1][0]).toBe('c')
});

test('move 2 crates on top of another', () => {
    let s = new stacks(10);
    s.stacks[1].push('c')
    s.stacks[1].push('d')
    s.stacks[3].push('x')

    s.move(2, 4, 2);

    expect(s.stacks[1].length).toEqual(0);
    expect(s.stacks[3].length).toEqual(3);
    expect(s.stacks[3][0]).toBe('x')
    expect(s.stacks[3][1]).toBe('d')
    expect(s.stacks[3][2]).toBe('c')
});
