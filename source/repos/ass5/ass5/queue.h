#ifndef __QUEUE_H
#define __QUEUE_H

#include "element.h"
#include "stack.h"

typedef struct {
	Stack* s1;
	Stack* s2;
} Queue;

Queue* initQueue();

void destroyQueue(Queue* queue);

int isQueueEmpty(Queue* queue);
int lenOfQueue(Queue* queue);
int capacityOfQueue(Queue* queue);

void enqueue(Queue* queue, Element element);
Element dequeue(Queue* queue);
Element peek(Queue* queue);

#endif
