const fs = require('fs');
const Buffer = require('./DataStreamBuffer')

class StartDetectorWidget {
    constructor(file, frameSize) {
        this.contents = fs.readFileSync(file);
        this.buffer = new Buffer(this.contents, frameSize)
    }

    IsStart() {
        let frame = this.buffer.frame;
        for (let outer = 0; outer < frame.length; outer++) {
            for (let inner = outer + 1; inner < frame.length; inner++) {
                if (frame[outer] == frame[inner]) {
                    return false;
                }
            }


        }

        return true;

    }

    FindPacketStart() {

        let foundStart = this.IsStart();
        while (!foundStart) {
            if (this.buffer.MoveFrameRight(1))
                foundStart = this.IsStart();
            else
                return -1;
        }
        return this.buffer.frameEnd + 1;
    }


}



module.exports = StartDetectorWidget