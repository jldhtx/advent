class DataStreamBuffer {
    constructor(input, frameSize) {
        this.input = input;
        this.frameSize = frameSize;
        this.frame = []
        this.frameStart = 0;
        this.frameEnd = frameSize - 1;
        this.SetFrame();
    }

    MoveFrameRight(places) {
        if (!this.ValidatePlaces(places)) return false;
        this.frameStart += places;
        this.frameEnd += places;
        this.SetFrame();
        return true;

    }

    SetFrame() {
        this.frame = []
        for (let index = this.frameStart; index <= this.frameEnd; index++) {
            this.frame.push(this.input[index]);
        }
    }
    ValidatePlaces(places) {
        let start = this.frameStart + places;
        if (start >= this.input.length)
            return false;
        return true;
    }

    AreEqual(left, right) {
        return this.frame[left] == this.frame[right];
    }

    PrintFrame() {
        let frame = this.frame;
        let outputBuffer = [''];
        for (let index = 0; index < this.frameSize; index++) {
            outputBuffer.push(String.fromCharCode(frame[index]));
        }
        console.log(`C U R R E N T   F R A M E  ${outputBuffer.join('')}`)
    }

}

module.exports = DataStreamBuffer