const buffer = require('./DataStreamBuffer.js');

test('constructor properly initializes frame', () => {
    let b = new buffer('abcd', 4);

    expect(b.frame.length).toBe(4);
    expect(b.frame[0]).toBe('a');
})

test('move frame right 1 place', () => {
    let b = new buffer('abcde', 4);

    expect(b.frame.length).toBe(4);
    expect(b.frame[0]).toBe('a');
    b.MoveFrameRight(1);
    expect(b.frame.length).toBe(4);
    expect(b.frame[0]).toBe('b');
    expect(b.frame[3]).toBe('e');
})


test('move frame right 2 places', () => {
    let b = new buffer('abcdefg', 4);

    expect(b.frame.length).toBe(4);
    expect(b.frame[0]).toBe('a');
    b.MoveFrameRight(2);
    expect(b.frame.length).toBe(4);
    expect(b.frame[0]).toBe('c');
    expect(b.frame[3]).toBe('f');
})

test('areequal true', () => {
    let b = new buffer('abcd', 4);
    expect(b.AreEqual(0, 1)).toBe(false);
})

test('areequal true', () => {
    let b = new buffer('abca', 4);
    expect(b.AreEqual(0, 3)).toBe(true);
})

test('moveRight past size', () => {
    let b = new buffer('abcd', 4);
    expect(b.MoveFrameRight(10)).toBe(false);
})
