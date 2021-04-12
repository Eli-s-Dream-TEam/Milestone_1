/***********
* Aviv Ben Yaakov
* 206261695
* 01
* ass05
***********/
#define _CRT_SECURE_NO_WARNINGS
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include "element.h"
#include "stack.h"
#include "strings.h"

/******************
* Function Name: isLegalString
* Input: char 
* Output: 1 - true or 0 - false
* Function Operation: the function recieves a string and check if the parentheses are balanced using stack.
- every open parenthesis must have matching closing one, and on the contrary.
- first parenthesis to open, will be the last to be closed.
- last parenthesis to open, will be the first to be closed.
when check complete, free all alocated memory.
******************/
int isLegalString(char str[]) {
	Stack* stack = initStack();
	Element element;
	for (int i = 0; i < strlen(str); i++) {
		// if char is closing parentheses and the stack is empty return 0
		if (((str[i] == '}') || (str[i] == ']') || (str[i] == ')') || (str[i] == '>')) && isStackEmpty(stack)) {
			destroyStack(stack);
			return 0;
		}
		// if the closing parentheses doesn't match to the openning one at the top of the stack return 0   
		if ((str[i] == '}') && (top(stack).c != '{') || (str[i] == ']') && (top(stack).c != '[')
			|| (str[i] == ')') && (top(stack).c != '(') || (str[i] == '>') && (top(stack).c != '<')) {
			destroyStack(stack);
			return 0;
		}
		// if the parentheses match - pop the openning parenthese out of the stack    
		if (str[i] == '}' && (top(stack)).c == '{') {
			pop(stack);
		}
		else if (str[i] == ']' && (top(stack)).c == '[') {       
			pop(stack);
		}
		else if (str[i] == ')' && (top(stack)).c == '(') {          								
			pop(stack);
		}
		else if (str[i] == '>' && (top(stack)).c == '<') {
			pop(stack);
		}
		// if char is openning parenthese push it into the stack
		else if ((str[i] == '{') || (str[i] == '[') || (str[i] == '(') || (str[i] == '<')) {
			element.c = str[i];
			push(stack, element);
		}
		
	}

	// stack must be empty at the end of the process
	if (!isStackEmpty(stack)) {
		destroyStack(stack); 
		return 0;
	}
	destroyStack(stack);
	return 1;
}


