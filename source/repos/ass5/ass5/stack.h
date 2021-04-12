#ifndef __STACK_H
#define __STACK_H

#include "element.h"

typedef struct {
	Element* content;
	int size;
	int topIndex;
} Stack;

Stack* initStack();
void destroyStack(Stack* stack);

int isStackEmpty(Stack* stack);
int lenOfStack(Stack* stack);
int capacityOfStack(Stack* stack);

void push(Stack* stack, Element element);
Element pop(Stack* stack);
Element top(Stack* stack);

void printStack(Stack* stack);

#endif
