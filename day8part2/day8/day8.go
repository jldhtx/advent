package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Println("Starting day 8!")
	fmt.Println("Contents:")

	horizontalGrid := getArrayFromFile("./input.test")
	verticalGrid := getColumns(horizontalGrid)
	length := len(horizontalGrid[0])
	scores := make([][]int, length)
	verticalScores := make([][]int, length)

	for row := 0; row < length; row++ {
		scores[row] = getScenicScores(horizontalGrid[row])
	}

	for column := 0; column < length; column++ {
		verticalScores[column] = getScenicScores(verticalGrid[column])
	}

	highestScore, x, y := 0, 0, 0

	for row := 0; row < length; row++ {
		for column := 0; column < length; column++ {
			score := scores[row][column] * verticalScores[column][row]
			if score > highestScore {
				highestScore = score
				x = row
				y = column
			}
		}
	}
	printGrid(scores, false)
	fmt.Println("----")
	printGrid(verticalScores, true)
	fmt.Println("----")
	fmt.Println("Highest scenic score: ")

	fmt.Println(fmt.Sprintf(" %d  ", highestScore))
	fmt.Println(fmt.Sprintf(" at [%d,%d]  ", x+1, y+1))

}

func getHighestScore(grid [][]int) (int, int, int) {
	length := len(grid[0])
	highest, row, column := 0, 0, 0
	for i := 0; i < length; i++ {
		for j := 0; j < length; j++ {
			if grid[i][j] > highest {
				highest = grid[i][j]
				row = i
				column = j
			}
		}
	}
	return highest, row, column
}
func printGrid(grid [][]int, flip bool) {
	length := len(grid[0])
	for i := 0; i < length; i++ {
		for j := 0; j < length; j++ {
			if flip {
				fmt.Print(fmt.Sprintf(" %d  ", grid[j][i]))
			} else {
				fmt.Print(fmt.Sprintf(" %d  ", grid[i][j]))
			}

		}
		fmt.Println()
	}
}

func getColumns(grid [][]int) [][]int {
	length := len(grid[0])
	columns := make([][]int, length)
	for i := 0; i < length; i++ {
		columns[i] = make([]int, length)
		for j := 0; j < length; j++ {
			columns[i][j] = grid[j][i]
		}
	}
	return columns
}

func getScenicScores(line []int) []int {
	length := len(line)
	scores := make([]int, length)
	for i := 0; i < length; i++ {
		height := line[i]
		scores[i] = getScenicScore(line[:i], height, true) * getScenicScore(line[i+1:], height, false)
	}
	return scores
}

func max(a, b int) int {
	if a <= b {
		return a
	}
	return b
}

func getScenicScore(line []int, height int, reverse bool) int {
	length := len(line)
	score := 0
	if length == 0 {
		return score
	}
	if !reverse {
		score += lookRight(line, height)
	} else {
		score += lookLeft(line, height)
	}
	return score
}

func lookRight(line []int, height int) int {
	score := 0
	length := len(line)
	for i := 0; i < length; i++ {
		if line[i] >= height {
			return score + 1
		}
		score++
	}
	return score
}

func lookLeft(line []int, height int) int {
	score := 0
	length := len(line)
	for i := length - 1; i >= 0; i-- {
		if line[i] >= height {
			return score + 1
		}
		score++
	}
	return score
}

/*
3 0 3 7 3
2 5 5 1 2
6 5 3 3 2
3 3 5 4 9
3 5 3 9 0
*/
// func setVisible([][]bool flags, [][]int grid){
// 	for i := 0; i < length; i++ {
// 		line := lines[i]
// 		a[i] = make([]int, length)
// 		for j := 0; j < length; j++{
// 			ch := line[j]
// 			x,e := strconv.Atoi(string(ch))
// 			check(e)
// 			a[i][j] = x;
// 		}

// 	}
// }

func getArrayFromFile(filename string) [][]int {
	contents := getInput(filename)
	lines := strings.Split(contents, "\n")
	length := len(lines[0])
	a := make([][]int, length)

	for i := 0; i < length; i++ {
		line := lines[i]
		a[i] = make([]int, length)
		for j := 0; j < length; j++ {
			ch := line[j]
			x, e := strconv.Atoi(string(ch))
			check(e)
			a[i][j] = x
		}

	}
	return a
}
func getInput(file string) string {
	data, err := os.ReadFile(file)
	contents := string(data)
	check(err)
	return contents
}

func check(e error) {
	if e != nil {
		panic(e)
	}
}
