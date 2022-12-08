const widget = require('./StartDetectorWidget');

test('e2e test', () => {
    let w = new widget('testInput', 4)
    let start = w.FindPacketStart();
    expect(start).toBe(6)
})

test('e2e test part2', () => {
    let w = new widget('input', 14)
    let start = w.FindPacketStart();
    expect(start).toBe(19)
})
