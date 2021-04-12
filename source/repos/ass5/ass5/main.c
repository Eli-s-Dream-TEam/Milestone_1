#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include "stack.h"
#include "queue.h"
#include "element.h"
#include "strings.h"

void manageStack() {
	Stack* stack = initStack();
	if (stack == NULL) {
		printf("Sorry, not today\n");
		return;
	}
	printf("Welcome to the elements' stack!\n");
	int input;
	do {
		Element element;
		printf("Please choose option:\n  1.push\n  2.top\n  3.pop\n  4.print stack\n  0.exit\n");
		scanf("%d", &input);
		if (input == 1) {
			printf("Enter element to push: ");
			scanElement(&element);
			push(stack, element);
		}
		else if (input == 2) {
			if (!isStackEmpty(stack)) {
				printf("top: ");
				printElement(top(stack));
				printf("\n");
			}
			else {
				printf("can't top! stack is empty!\n");
			}
		}
		else if (input == 3) {
			if (!isStackEmpty(stack)) {
				printf("pop: ");
				printElement(pop(stack));
				printf("\n");
			}
			else {
				printf("can't pop! stack is empty!\n");
			}
		}
		else if (input == 4) {
			printStack(stack);
		}
	} while (1 <= input && input <= 4);
	destroyStack(stack);
}

void manageQueue() {
	Queue* queue = initQueue();
	if (queue == NULL) {
		printf("Sorry, not today\n");
		return;
	}
	printf("Welcome to the elements' queue!\n");
	int input;
	do {
		Element element;
		printf("Please choose option:\n  1.enqueue\n  2.peek\n  3.dequeue\n  4.num of elements\n  0.exit\n");
		scanf("%d", &input);
		if (input == 1) {
			printf("Enter element to enqueue: ");
			scanElement(&element);
			enqueue(queue, element);
		}
		else if (input == 2) {
			if (!isQueueEmpty(queue)) {
				printf("peek: ");
				printElement(peek(queue));
				printf("\n");
			}
			else {
				printf("can't peek! queue is empty!\n");
			}
		}
		else if (input == 3) {
			if (!isQueueEmpty(queue)) {
				printf("dequeue: ");
				printElement(dequeue(queue));
				printf("\n");
			}
			else {
				printf("can't dequeue! queue is empty!\n");
			}
		}
		else if (input == 4) {
			printf("queue length: %d\n", lenOfQueue(queue));
		}
	} while (1 <= input && input <= 4);
	destroyQueue(queue);
}

void manageStrings() {
	char* strings[] = {
		"{[(<>)]}",
		"()()()",
		"]()()()[",
		"=[]()()()[]=",
		"{}}",
		"{[()]}",
		"{{}",
		"((()))",
		"{999967(})",
		"",
		"<<578>",
		"33(<{}(){}>)83",
		"{}35{}}}",
		"{999<99>95}",
		"{]",
		"</n>",
		"[][][][]<",
		"([]<{}>[])",
		"(()",
		"<{()()()}>",
		")",
		"<><><>{{}}()()()",
		"(",
		"[<()>]",
		"><",
		"[====]",
		"((({}[{ }}])({})))"
	};
	int testLen = sizeof(strings) / sizeof(char*);
	for (int i = 0; i < testLen; i++) {
		char* str = strings[i];
		printf("%s\n", isLegalString(str) ? "Legal" : "Illegal");
	}
}


void main() {
	int input;
	printf("Choose program:\n  1.Stack manager\n  2.Queue manager\n  3.String validator\n");
	scanf("%d", &input);
	if (input == 1) {
		manageStack();
	}
	else if (input == 2) {
		manageQueue();
	}
	
	else if (input == 3) {
		manageStrings();
	}
	
}
