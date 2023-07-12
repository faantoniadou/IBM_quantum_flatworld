<template>
    <Menubar :model="items">
        <template #start>
            <img alt="logo" src="/assets/logo.png" height="40" class="mr-2" />
        </template>
        <template #end>
            <InputText placeholder="Search" type="text" />
        </template>
    </MenuBar>
</template>

<script>
import Menubar from 'primevue/menubar';
import InputText from 'primevue/inputtext';

export default {
    components: {
        Menubar,
        InputText
    },
    data() {
        return {
            items: [
                // Your menu items here...
            ]
        };
    }
}   
</script>

<style scoped lang="scss">
    // Your styles here...
</style>
