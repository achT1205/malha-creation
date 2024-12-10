import { defineStore } from 'pinia';
import { ref, computed } from 'vue'

// Utilitaire pour gérer le stockage dans localStorage
const useLocalStorage = (key, initialValue) => {
  const storedValue = JSON.parse(localStorage.getItem(key));
  return storedValue !== null ? storedValue : initialValue;
};


export const useCartStore = defineStore('cart', () => {
    const items = ref( useLocalStorage('cartItems', []))
    function addItem(product) {
        const existingItem = items.value.find(item => item.id === product.id && product.color === item.color && product.size === item.size);
        if (existingItem) {
          existingItem.quantity += 1;
        } else {
          items.value.push(product);
        }
        saveCart();
        hasDiscount(product)
    }

    function hasDiscount (product) {
        if(product.discount){
          let totalQuantity = 0
          const filtered = items.value.filter(_=>_.slug === product.slug)
          totalQuantity = filtered.reduce((sum, current) => sum + parseInt(current.quantity), 0)
          if(totalQuantity >= product.discount.multiple)
          {
            if((totalQuantity % product.discount.multiple) === 0)
            {
                items.value.filter(_=>_.slug === product.slug).forEach(_=>{
                 _.discountPrice = _.price * _.quantity - (_.price *_.quantity* product.discount.rate) / 100  
                })
            }
            else 
                items.value.filter(_=>_.slug === product.slug).forEach(_=>{
                if((_.quantity % product.discount.multiple) === 0)
                _.discountPrice = _.price * _.quantity - (_.price *_.quantity* product.discount.rate) / 100
                else 
                _.discountPrice =  ((parseInt(_.quantity /product.discount.multiple) *product.discount.multiple * _.price ) - (parseInt(_.quantity /product.discount.multiple) *product.discount.multiple * _.price * product.discount.rate /100)) + _.price 
            })



          }else {
            items.value.filter(_=>_.slug === product.slug).forEach(_=>delete _.discountPrice)
          }
        }
          saveCart()
        }
    function editItem(product, quantity) {
        const index = items.value.findIndex(item => item.id === product.id && product.color === item.color && product.size === item.size);
        items.value[index].quantity = quantity
        delete items.value[index].discountPrice
        saveCart();
        hasDiscount(product)
    }
    function removeItem(product) {
        const index = items.value.findIndex(item => item.id === product.id && product.color === item.color && product.size === item.size);
        items.value.splice(index, 1)
        items.value.filter(_=>_.slug === product.slug).forEach(_=> delete _.discountPrice)
        saveCart();
        hasDiscount(product)
    }
    function clearCart() {
        items.value = [];
        saveCart();
    }
    function  saveCart() {
        localStorage.setItem('cartItems', JSON.stringify(items.value));
    }
    function loadCart() {
        items.value = useLocalStorage('cartItems', []);
    }
    
    const itemCount = computed(() =>items.value.length)
    const totalPrice = computed(()=> items.value.reduce((total, item) => total + item.price * item.quantity, 0))
    const totalDiscountPrice = computed(()=> items.value.reduce((total, item) => item.discountPrice && item.discountPrice > 0 ? total + item.discountPrice : total + item.price * item.quantity, 0))

    
    return { addItem, removeItem, clearCart, saveCart , loadCart, itemCount, totalPrice, totalDiscountPrice, items, editItem }
})
