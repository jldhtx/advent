class CraneCommand {

    constructor(command) {

        // move 1 from 2 to 1
        const pattern = /move\s([0-9]+)\sfrom\s([0-9]+)\sto\s([0-9]+)/;
        const match = command.match(pattern);
        this.move = parseInt(match[1]);
        this.from = parseInt(match[2]);
        this.to = parseInt(match[3]);
    }

}

module.exports = CraneCommand;