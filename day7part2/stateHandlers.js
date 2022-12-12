
const lsPattern = /\$\sls/g;
const directoryPattern = /dir\s(.*)/;
const directoryNamePattern = /cd\s(.*)/;
const filePattern = /(\d+)\s(.*)/;


const stateHandlers = {
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

const test = (input, pattern) => {
    return pattern.test(input);
}


module.exports = stateHandlers