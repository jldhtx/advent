class CrateStacks {
    stacks = []
    constructor(size) {
        for (let index = 0; index < size; index++) {
            this.stacks.push([]);
        }
        console.log(`Initiated ${size} empty stacks for crates!`)
    }

    add(stack, item) {
        this.stacks[stack - 1].push(item);
        console.log(`Added ${item} to stack ${stack}`)
    }

    move(from, to, count) {

        console.log(`Moving ${count} crates from stack ${from} to stack ${to}`)

        let f = from - 1;
        let t = to - 1;

        for (let i = 0; i < count; i++) {
            this.stacks[t].push(this.stacks[f].pop());
        }
    }

    output() {
        for (let i = 0; i < this.stacks.length; i++) {
            let line = `stack ${i + 1}\t`
            for (let j = 0; j < this.stacks[i].length; j++) {
                line = line + `${this.stacks[i][j]}`
            }
            console.log(line);
        }
    }



}

module.exports = CrateStacks;