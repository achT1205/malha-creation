import { reactive } from 'vue';

const toasts = reactive([]);

export function useToast() {
  const add = (toast) => {
    toasts.push(toast);
    if (toast.life) {
      setTimeout(() => {
        remove(toast);
      }, toast.life);
    }
  };

  const remove = (toast) => {
    const index = toasts.indexOf(toast);
    if (index !== -1) {
      toasts.splice(index, 1);
    }
  };

  return {
    toasts,
    add,
  };
}
