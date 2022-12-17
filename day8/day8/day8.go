package main

import "fmt"
import "os"
import "strings"
import "strconv"

func main() {
    fmt.Println("Starting day 8!")
	fmt.Println("Contents:")
	
	horizontalGrid := getArrayFromFile("./input.test");
	verticalGrid := getColumns(horizontalGrid)
	length := len(horizontalGrid[0])
	flags := make([][]bool,length)
	
	for row := 0; row < length; row++ {
		flags[row] = getVisibleTrees(horizontalGrid[row])
	}

	for column := 0; column < length; column++ {
		verticalFlags := getVisibleTrees(verticalGrid[column])
		for row := 0; row < length;row++{
			if verticalFlags[row] == true {
				flags[row][column] = true
			}

		}
	}

	printGrid(horizontalGrid,flags)
	fmt.Println("Total visible:")
	fmt.Println(getTotalVIsible(flags))

}

func getTotalVIsible(grid [][]bool ) int {
	length := len(grid[0])
	count := 0
	for i := 0; i < length; i++ {
		for j := 0; j < length; j++{
			if grid[i][j]{
				count++
			}
		}
	}
	return count;
}
func printGrid(grid [][]int, visible [][]bool){
	length := len(grid[0])
	for i := 0; i < length; i++ {
		for j := 0; j < length; j++{
			if (visible[i][j]){
				fmt.Print(fmt.Sprintf(" %d* ",grid[i][j]))	
			} else {
			fmt.Print(fmt.Sprintf(" %d  ",grid[i][j]))
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
		for j := 0; j < length; j++{
			columns[i][j] = grid[j][i]
	}
}
	return columns;
}

func getVisibleTrees(line []int) []bool{
	length := len(line)
	flags := make([]bool,length)
	for i := 0; i < length; i++ {
		height := line[i]
		flags[i] = isVisible(line[:i],height) || isVisible(line[i+1:], height)
	}
	return flags;
}

func isVisible(line []int, height int) bool{
	length := len(line)
	if length == 0 {
		return true;
	}
	for i := 0; i < length; i++ {
		if(line[i] >= height) {
			return false;
		}
	}
	return true;
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
	contents:= getInput(filename)
	lines:=strings.Split(contents,"\n")
	length:=len(lines[0]);
	a := make([][]int, length) 

	for i := 0; i < length; i++ {
		line := lines[i]
		a[i] = make([]int, length)
		for j := 0; j < length; j++{
			ch := line[j]
			x,e := strconv.Atoi(string(ch))
			check(e)
			a[i][j] = x;	
		}
		
	}
	return a
}
func getInput(file string) string {
	data,err := os.ReadFile(file)
	contents := string(data)
	check(err)
	return contents
}

func check(e error) {
    if e != nil {
        panic(e)
    }
}