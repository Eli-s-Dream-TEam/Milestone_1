/***********
* Aviv Ben Yaakov
* 206261695	
* 01
* ass05
***********/
#define _CRT_SECURE_NO_WARNINGS
#include <stdlib.h>
#include <stdio.h>
#include "element.h"
#include "stack.h"


/******************
* Function Name: initStack
* Input:none
* Output:return initialized stack
* Function Operation: this function is initialized stack 
******************/
Stack* initStack()
{
	// allocate memory for Stack struct 
	Stack* stack = (Stack*)malloc(sizeof(Stack));
	// in case allocation fail
	if (stack == NULL) {
		printf("Stack: initStack: Heap memory error!\n");
		return NULL;
	}
	stack->topIndex = -1; // index of element inside the stack, set to -1 to represent empty stack
	stack->size = 1; // size of empty stack
	// allocate memory for content member 
	stack->content = (Element*)malloc(sizeof(Element));
	// in case allocation fail
	if (stack->content == NULL) {
		printf("Stack: initStack: Heap memory error!\n");
		return NULL;
	}

	return stack;
}

/*
this is function for free the memory at the end of the process 
*/
void destroyStack(Stack* stack) {
	free(stack->content);
	free(stack);
}

/******************
* Function Name: isStackEmpty
* Input: initialized stack
* Output: 1 - true or 0 - false
* Function Operation: this function check whether the stack is empty or not.
if empty - return 1, else - return 0.
******************/
int isStackEmpty(Stack* stack) {
	return stack->topIndex == -1; // topIndex = -1  -> empty stack
}
/*
this function recieves initialized stack and returns the capacity of stack represented by size memeber
*/
int capacityOfStack(Stack* stack) {
	return stack->size;
}

// this function returns the current number of elemnets in the stack
int lenOfStack(Stack* stack) {
	return stack->topIndex + 1; // number of elements inside the stack represented by topIndex + 1
}

/******************
* Function Name: push
* Input: initialized stack and one element
* Output: none, a void function
* Function Operation: this function get an element and push it inside the stack.
- if necessary, allocate more memory to the stack.

******************/
void push(Stack* stack, Element element) {
	// if number of elements in stack equals to capacity of stack allocate more memory
	if (stack->size == lenOfStack(stack)) {
		stack->size *= 2; // double the size of stack
		Element* temp = stack->content; // save the element into temporary variable in case reallocation fail
		stack->content = (Element*)realloc(stack->content, sizeof(Element) * stack->size); // allocate more memory
		// in case memory allocation failed
		if (stack->content == NULL) {
			printf("Stack: push: Heap memory error!");
			stack->content = temp;
		}
	}
	++(stack->topIndex); // increase topIndex value by one
	// set the top element as the current one  
	stack->content[stack->topIndex] = element;
}

/******************
* Function Name: pop
* Input:initialized stack
* Output: one element
* Function Operation: this function pulls out the current top element of the stack and return it to the user.
- if necessary, free some of the memory.
******************/
Element pop(Stack* stack) {
	Element topElement = stack->content[stack->topIndex]; 
	// if half of the stack empty free half of the allocated memory
	if (stack->size == (lenOfStack(stack) * 2)) {
		stack->size /= 2; // cut in half the size of the stack
		Element *temp = stack->content; // save the element into temporary  variable in case allocation fail
		stack->content = (Element*)realloc(stack->content, sizeof(Element) * stack->size);
		// in case memory allocation failed
		if (stack->content == NULL) {
			printf("Stack: pop: Heap memory error!");
			stack->content = temp;
		}
	}
	--(stack->topIndex); // decrease topIndex value by one
	return topElement; 
}

// this function return the top element of the stack to the user, without changing the stack.
Element top(Stack* stack) {
	return stack->content[stack->topIndex]; 
}

/* 
this function print the current elements inside the stack in the order of insertion.
this function use function printElement for printing the current elements inside the stack.
*/
void printStack(Stack* stack) {
	int lastIndex = stack->topIndex;
	for (int i = lastIndex ; i >= 0; i--){
		printf("%d: ", i + 1); // this number represent the index of inserted element 
		Element element;
		element = stack->content[i]; // set element as the current member inside the stack
		printElement(element);  
		printf("\n");
	}
}






