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
#include "queue.h"

/******************
* Function Name: initQueue
* Input:none
* Output:return initialized queue
* Function Operation: this function initialize queue(represented by two stacks) and returns the queue.
******************/
Queue* initQueue() {
	
	Queue* queue = (Queue*)malloc(sizeof(Queue));
	// in case memory allocatiion fail
	if (queue == NULL) {
		printf("Queue: initQueue: Heap memory error!\n");
		return NULL;
	}
	queue->s1 = initStack(); // initialized first stack
	queue->s2 = initStack(); // initialized second stack
	return queue; 
}

/*
this is function for free the memory at the end of the process
*/
void destroyQueue(Queue* queue) {
	destroyStack(queue->s1); // free s1
	destroyStack(queue->s2); // free s2
	free(queue); 
}

/******************
* Function Name: isQueueEmpty
* Input: initialized queue
* Output: 1 - true or 0 - false
* Function Operation: this function check whether queue is empty or not.
if empty - return 1, else - return 0.
******************/
int isQueueEmpty(Queue* queue) {
	// the queue is empty if each of the stacks is empty.
	return isStackEmpty(queue->s1) && isStackEmpty(queue->s2);
}

// this function recieves initialized queue and returns it length.
int lenOfQueue(Queue* queue) {
	// the length of queue represented by total number of elements inside the two stacks.
	return lenOfStack(queue->s1) + lenOfStack(queue->s2);
}

// this function recieves initialized queue and returns it capacity.
int capacityOfQueue(Queue* queue) {
	// the size of queue represented by combined size of the two stacks.
	return capacityOfStack(queue->s1) + capacityOfStack(queue->s2);
}

/******************
* Function Name: enqueue
* Input: initialized queue and one element.
* Output: none, a void function.
* Function Operation: this function recieves an initialized queue and one element and push the queue into s1.
******************/
void enqueue(Queue* queue, Element element) {
	push(queue->s1, element);
}

/******************
* Function Name: dequeue
* Input: initialized queue
* Output: element.
* Function Operation: this function pulls out the first inserted queue.
- if s2 is not empty, pull the top element(which represent the first queue).
- if s2 empty, pull one by one each element from s1 and insert it into s2, proceed until s1 is empty.
- pull the top element(which represent the first queue) of s2 and return it to the user.
******************/
Element dequeue(Queue* queue) {
	if(!isStackEmpty(queue->s2)) {
		return pop(queue->s2);
	}
	else {
		while (!isStackEmpty(queue->s1)) { // keep poping until s1 is empty.
			Element temp;
			temp = pop(queue->s1); // save element in temporary variable.
			push(queue->s2, temp); 
		}
	}
	return pop(queue->s2); // pull out the first queue and return it to the user.
}

/******************
* Function Name: peek
* Input: initialized queue
* Output: element.
* Function Operation: this function peek return the first queue to the user.
- if s2 is not empty, return it first element to the user.
- if s2 empty, pull one by one each element from s1 and insert it into s2, proceed until s1 is empty.
- return the first queue to the user.
******************/
Element peek(Queue* queue) {
	if (!isStackEmpty(queue->s2)) {
		return top(queue->s2);
	}
	else {
		while (!isStackEmpty(queue->s1)) { // keep poping until s1 is empty.
			Element temp;
			temp = pop(queue->s1); // save element in temporary variable.
			push(queue->s2, temp);
		}
	}
	return top(queue->s2); // return the first queue to the user.
}




