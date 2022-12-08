const fs = require('fs');
const { get } = require('http');

const f = fs.createReadStream('testInput');

const allFileContents = fs.readFileSync('testInput', 'utf-8')

let start = getStartOfPacket(0, allFileContents)

console.log(start)

function getStartOfPacket(start, contents) {
    if (start + 3 == contents.length)
        return -1;
    if (contents[start] == contents[start + 1])
        return getStartOfPacket(start + 1, contents);
    else if (contents[start] == contents[start + 2])
        return getStartOfPacket(start + 2, contents);
    else if (contents[start] == contents[start + 3])
        return getStartOfPacket(start + 3, contents);
    else if (contents[start + 1] == contents[start + 2])
        return getStartOfPacket(start + 2, contents);
    else if (contents[start + 1] == contents[start + 3])
        return getStartOfPacket(start + 3, contents);
    else if (contents[start + 2] == contents[start + 3])
        return getStartOfPacket(start + 3, contents);
}
/*


1 1 2 3
1 2 2 3
1 2 3 3
1 2 1 4
1 2 3 4


*/