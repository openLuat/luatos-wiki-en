# Memory Pool

## Basic information

* Date of drafting: 2019-11-25
* Designer: [wendal](https://github.com/wendal)

## Why you need a memory pool

* A continuous area is allocated to users, independent of the system's heap
* The size of this memory interval is 64k ~ 100k
* Lua Virtual machines and related global variables should use this zone

## Design ideas and boundaries

* using freertos heap_4 as a prototype
* An additional alloc method using the Lua virtual machine is provided.
* Provide API to query remaining memory
* API Should only involve memory application and release, do nothing else.

## C API

### Defines the total memory pool size

```c
#define LUAT_MALLOC_HEAP_SIZE ((size_t) 85 * 1024)
```

###

```c
// Initialize memory
void  luat_heap_init(void);
// Request Memory
void* luat_heap_malloc(size_t len); // If it fails, return NULL
// Release memory
void  luat_heap_free(void* ptr);
// Scale Memory Blocks
void* luat_heap_realloc(void* ptr, size_t len);
// Apply for memory and populate it 0
void* luat_heap_calloc(size_t len);
// Get Remaining Memory
size_t luat_heap_getfree(void);
// Lua Required alloc method
void* luat_heap_alloc(void *ud, void *ptr, size_t osize, size_t nsize);
```

## Lua API

```lua
-- Get Total Memory Amount
mem.total_count()
-- Get the amount of remaining memory
mem.free_count()
```

## Relevant knowledge points

* [Luat core mechanism](/markdown/core/luat_core)

