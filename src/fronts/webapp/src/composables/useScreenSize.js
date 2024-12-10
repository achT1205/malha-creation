import { ref, onMounted, onUnmounted } from 'vue';

export function useScreenSize() {
  const isMobile = ref(false);
  const isTablet = ref(false);
  const isDesktop = ref(false);

  const checkScreenSize = () => {
    const width = window.innerWidth;
    isMobile.value = width <= 768;
    isTablet.value = width > 768 && width <= 1024;
    isDesktop.value = width > 1024;
  };

  onMounted(() => {
    checkScreenSize();
    window.addEventListener('resize', checkScreenSize);
  });

  onUnmounted(() => {
    window.removeEventListener('resize', checkScreenSize);
  });

  return { isMobile, isTablet, isDesktop };
}
